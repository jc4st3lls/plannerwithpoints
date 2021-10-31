# POC: GEOMETRIA APLICADA A AGENDES (GEOMETRY APPLIED TO AGENDA)
## Planificar cites amb punts geomètrics

La prova de concepte intenta demostrar l’ús de la geometria per trobar forats buits en diferents  agendes el més pròxims possibles en el temps.
Com a exemple,  imaginem que hem de donar cites a un pacient d’un hospital per fer un Preoperatori (proves que s’ha de realitzar abans d’una intervenció quirúrgica). Suposem, que un preoperatori consta d’una analítica, un electrocardiograma i una visita a l’anestesista. Per facilitar-li la feina al pacient, mirarem de donar-li les tres cites, el mateix dia i amb el temps d’estada al Hospital el més reduït possible.
Més detallat tot en el document **POCGeometria.pdf** (adjunt al repositori).

## Fer correr el codi
Primer de tot desplegarem un contenidor docker amb SQL Server 2019 per linux

``docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=@thisIsAp0c" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest ``

Tot seguit entrem al contenidor i executem 

``/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P @thisIsAp0c``

``CREATE DATABASE PWP``

``GO``

``USE PWP``

``GO``

Tot seguit obrim un terminal i anem a la carpeta on hem descarregat el codi. Ens posicionem dins la carpeta del projecte DbMigrations. Executem:

``dotnet ef migrations add InitialCreate``

``dotnet ef database update``

Ara carreguem la solució, i anem al projecte PWPConsole. Dins la classe Program.cs, dins el Main, descomentem la crida al metode  **PopulateDb();** i executem.

Tot seguit comentem aquest mètode i descomentem els dels testos, i executem. Podrem veure els resultats.

Seguidament, podem fer més testos, afegir més agendes, més diataris, etc.
