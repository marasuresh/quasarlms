SET n2cms=c:\usr\src\eval\n2cms
SET n2cms_src=%n2cms%\src
SET n2cms_lib=%n2cms%\Lib

SET cssFriendly=c:\usr\src\eval\CssFriendly\CSSFriendly
SET dest=.\Lib

REM N2
COPY /Y %n2cms_src%\n2\obj\Debug\n2.dll %dest%\
COPY /Y %n2cms_src%\n2\obj\Debug\n2.pdb %dest%\

COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Templates.dll %dest%\
COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Templates.pdb %dest%\

COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Edit.dll %dest%\
COPY /Y %n2cms_src%\wwwroot\obj\Debug\N2.Edit.pdb %dest%\

COPY /Y %n2cms_src%\wwwroot\bin\N2.Security.dll %dest%\
COPY /Y %n2cms_src%\wwwroot\bin\N2.Security.pdb %dest%\

COPY /Y %n2cms_src%\N2.Tests\obj\Debug\N2.Tests.exe %dest%\

REM N2 dependencies
COPY /Y %n2cms_lib%\Castle.Core.dll %dest%\
COPY /Y %n2cms_lib%\Castle.DynamicProxy2.dll %dest%\
COPY /Y %n2cms_lib%\Castle.MicroKernel.dll %dest%\
COPY /Y %n2cms_lib%\Castle.Windsor.dll %dest%\

COPY /Y %n2cms_lib%\Iesi.Collections.dll %dest%\

COPY /Y %n2cms_lib%\NHibernate.dll %dest%\
COPY /Y %n2cms_lib%\NHibernate.Caches.SysCache2.dll %dest%\

COPY /Y %n2cms_lib%\System.Data.SQLite.dll %dest%\
COPY /Y %n2cms_lib%\log4net.dll %dest%\

COPY /Y %n2cms_lib%\nunit.framework.dll %dest%\

REM Css Friendly
COPY /Y %cssFriendly%\obj\Debug\CSSFriendly.dll %dest%\