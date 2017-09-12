using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de QueryString
/// </summary>

public class EncryptionUrl : IHttpModule
{
    ///
    /// variables const
    ///
    private const string nombreParametro = "q=";
    //private const string nombreParametro;
    private const string llaveEncriptacion = "key";

    ///
    /// Salt para "reforzar" encriptado
    ///
    private readonly static byte[] salt = Encoding.ASCII.GetBytes(llaveEncriptacion.Length.ToString());

    ///
    ///
    ///
    public EncryptionUrl()

    {
        //Algún día necesitaré esto lol
    }

    ///
    /// Implementamos Dispose de interfaz
    ///
    public void Dispose()
    {
        //Nothing
    }

    ///
    /// Implementamos Init de Interfaz
    /// Con esto construimos un nuevo evento para manejar la petición
    ///
    ///
    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    ///
    /// Evento que maneja la petición
    ///
    ///
    ///
    void context_BeginRequest(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current; //Contexto http actual

        if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?")) //url contiene ".aspx" && "?" ?
        {
            string query = ExtraerCadena(context.Request.RawUrl);
            string ruta = ObtenerRutaVirtual();

            if (query.StartsWith(nombreParametro, StringComparison.OrdinalIgnoreCase))
            {
                // Desencripta queryString y se vuelve a establecer la ruta
                string rawQuery = query.Replace(nombreParametro, string.Empty);
                string decryptedQuery = Desencriptar(rawQuery);
                context.RewritePath(ruta, string.Empty, decryptedQuery);
            }
            else if (context.Request.HttpMethod == "GET")
            {
                // Encripta queryString y reedirecciona a la url encriptada
                //Encripta todas las queryString automáticamente. ****Eliminar esta parte si no se desea encriptar automáticamente y realizar otras acciones
                string encryptedQuery = Encriptar(query);
                context.Response.Redirect(ruta + encryptedQuery);
            }
        }
    }

    ///
    /// Analiza la url actual y extrae la ruta virtual sin usar la queryString
    ///
    /// Ruta virtual de la url actual
    private static string ObtenerRutaVirtual()
    {
        string ruta = HttpContext.Current.Request.RawUrl;
        ruta = ruta.Substring(0, ruta.IndexOf("?"));
        ruta = ruta.Substring(ruta.LastIndexOf("/") + 1);
        return ruta;
    }

    ///
    /// Analiza la url y regresa la queryString
    ///
    ///url a analizar
    /// QueryString sin signo "?"
    private static string ExtraerCadena(string url)
    {
        int indice = url.IndexOf("?") + 1;
        return url.Substring(indice);
    }

    ///
    /// Encripta cualquier cadena con el algoritmo Rijndael.
    ///
    ///Cadena a encriptar
    /// Cadena encriptada Base64
    public static string Encriptar(string cadenaEntrada)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] textoPlano = Encoding.Unicode.GetBytes(cadenaEntrada);
        PasswordDeriveBytes llave = new PasswordDeriveBytes(llaveEncriptacion, salt);

        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(llave.GetBytes(32), llave.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(textoPlano, 0, textoPlano.Length);
                    cryptoStream.FlushFinalBlock();
                    return "?" + nombreParametro + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

    ///
    /// Desencripta la cadena encriptada previamente
    ///
    ///Cadena a desencriptar
    /// Cadena desencriptada
    public static string Desencriptar(string cadenaEntrada)
    {
        try
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] datosCifrados = Convert.FromBase64String(cadenaEntrada);
            PasswordDeriveBytes llave = new PasswordDeriveBytes(llaveEncriptacion, salt);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(llave.GetBytes(32), llave.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(datosCifrados))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] textoPlano = new byte[datosCifrados.Length];
                        int contador = cryptoStream.Read(textoPlano, 0, textoPlano.Length);
                        return Encoding.Unicode.GetString(textoPlano, 0, contador);
                    }
                }
            }
        }
        //Por implementación rápida, cualquier error o cualquier dato extra que introduzca el usuario en la url encriptada o algo por el estilo, lo enviaré a la misma //página de error personalizada
        catch (FormatException)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Redirect("wfrmErrorPagina.aspx");
            return null;
        }
        catch (CryptographicException)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Redirect("wfrmErrorPagina.aspx");
            return null;
        }
        catch (IndexOutOfRangeException)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Redirect("wfrmErrorPagina.aspx");
            return null;
        }
        finally
        {
            //Destruimos o finalizamos lo que necesitemos. No olvidar que existe el método Dispose ;)
        }
    }
}