using System;

using System.Globalization;

using System.Web.UI;
using System.Web.UI.WebControls;

//A customized class for displaying the Template Column
public class TextBoxTemplate : ITemplate
{
    //A variable to hold the type of ListItemType.
    ListItemType _templateType;

    //A variable to hold the column name.
    string _columnName;
    bool _enabled;
    string _css;
    string _key;
    string _funcionJS;
    //string _id;
    //Constructor where we define the template type and column name.
    public TextBoxTemplate(ListItemType type, string colname, bool enabled, string css,string key,string funcionJS)//, string id
    {
        //Stores the template type.
        _templateType = type;

        //Stores the column name.
        _columnName = colname;

        //Stores the enabled.
        _enabled = enabled;
        //Stores the CSS
        _css = css;

        _key = key;
        _funcionJS = funcionJS;
        //_id = id;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case ListItemType.Header:
                //Creates a new label control and add it to the container.
                Label lbl = new Label();            //Allocates the new label object.
                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                break;

            case ListItemType.Item:
                //Creates a new text box control and add it to the container.
                System.Web.UI.WebControls.TextBox tb1 = new TextBox();//Allocates the new text box object.
                
                tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                //tb1.Width = new Unit(80);                              //Style
                tb1.CssClass = _css;
                tb1.Enabled = _enabled;
                //tb1.ID = _id;
                tb1.Attributes.Add(_key, _funcionJS);
                tb1.MaxLength = 9;
                container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.
                break;

            case ListItemType.EditItem:
                //Added any code here.
                break;

            case ListItemType.Footer:
                TextBox tb2 = new TextBox();//Allocates the new text box object.
                tb2.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                //tb1.Width = new Unit(80);                              //Style
                tb2.CssClass = _css;
                tb2.Enabled = _enabled;
                container.Controls.Add(tb2);
                break;
        }
    }

    /// <summary>
    /// This is the event, which will be raised when the binding happens.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tb1_DataBinding(object sender, EventArgs e)
    {
        TextBox txtdata = (TextBox)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        if (dataValue != DBNull.Value)
        {
            decimal dato = Convert.ToDecimal(dataValue); //CC. Segun especificaciones
            txtdata.Text = dato.ToString("C", CultureInfo.CurrentCulture); //Coloque adecuado a la Informacion que Manejara la Vista
        }
    }
}