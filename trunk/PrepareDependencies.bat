SET n2cms=c:\usr\src\eval\n2cms
SET n2cms_src=%n2cms%\src
SET n2cms_lib=%n2cms%\Lib

SET cssFriendly=c:\usr\src\eval\CssFriendly\CSSFriendly
SET comfortAsp=C:\usr\src\eval\_lib\ComfortASP\ASP20

SET LIB=.\Lib

REM N2
COPY /Y %n2cms_src%\n2\obj\Debug\n2.dll %LIB%\
COPY /Y %n2cms_src%\n2\obj\Debug\n2.pdb %LIB%\

COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Templates.dll %LIB%\
COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Templates.pdb %LIB%\

COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Edit.dll %LIB%\
COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Edit.pdb %LIB%\

COPY /Y %n2cms_src%\wwwroot\bin\N2.Security.dll %LIB%\
COPY /Y %n2cms_src%\wwwroot\bin\N2.Security.pdb %LIB%\

COPY /Y %n2cms_src%\N2.Tests\obj\Debug\N2.Tests.exe %LIB%\

REM N2 dependencies
COPY /Y %n2cms_lib%\Castle.Core.dll %LIB%\
COPY /Y %n2cms_lib%\Castle.DynamicProxy2.dll %LIB%\
COPY /Y %n2cms_lib%\Castle.MicroKernel.dll %LIB%\
COPY /Y %n2cms_lib%\Castle.Windsor.dll %LIB%\

COPY /Y %n2cms_lib%\Iesi.Collections.dll %LIB%\

COPY /Y %n2cms_lib%\NHibernate.dll %LIB%\
COPY /Y %n2cms_lib%\NHibernate.Caches.SysCache2.dll %LIB%\

COPY /Y %n2cms_lib%\System.Data.SQLite.dll %LIB%\
COPY /Y %n2cms_lib%\log4net.dll %LIB%\

COPY /Y %n2cms_lib%\nunit.framework.dll %LIB%\

REM Css Friendly
COPY /Y %cssFriendly%\obj\Debug\CSSFriendly.dll %LIB%\

REM ComfortASP
COPY /Y %comfortAsp%\ComfortASP.dll %LIB%\