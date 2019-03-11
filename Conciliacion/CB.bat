@Echo OFF
Echo Start Time - %Time%
Echo Start Time - %Time% > cb.log
Echo git checkout SigaMetClasses
git -C "C:\Users\Ricardo Rojas\Source\Repos\RTGMGateway" checkout development >> cb.log

Echo git pull SigaMetClasses
git -C "C:\Users\Ricardo Rojas\Source\Repos\RTGMGateway" pull >> cb.log

Echo Build RTGMGateway
SET SolutionPath="C:\Users\Ricardo Rojas\Source\Repos\RTGMGateway\RTGMGateway\RTGMGateway.sln"
MSbuild %SolutionPath% /p:Configuration=Release /p:Platform="Any CPU" >> cb.log
Set /p Wait=Enter para continuar

Echo git checkout Conciliacion Bancaria
git -C "C:\Users\Ricardo Rojas\Source\Repos\conciliacionbancaria\Conciliacion\App_Web\Sitio Conicliacion 07-07-2017" checkout RM_Bugfix_CargaExcel_16_11_2018 >> cb.log

Echo git pull Conciliacion Bancaria
git -C "C:\Users\Ricardo Rojas\Source\Repos\conciliacionbancaria\Conciliacion\App_Web\Sitio Conicliacion 07-07-2017" pull >> cb.log

Echo MSbuild Conciliacion Bancaria
SET SolutionPath="C:\Users\Ricardo Rojas\Source\Repos\conciliacionbancaria\Conciliacion\App_Web\Sitio Conicliacion 07-07-2017\SolucionConciliacion.sln"
MSbuild %SolutionPath% /p:Configuration=Release /p:Platform="Any CPU" >> cb.log

Echo Empaquetando Archivos Version %1
winrar a -m5 App_Web_%1.rar app_web

Echo End Time - %Time%
Echo End Time - %Time% >> cb.log
Set /p Wait=Compilación completada...