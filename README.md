# POC: GEOMETRIA APLICADA A AGENDES
## Planificar cites amb punts geomètrics

La prova de concepte intenta demostrar l’ús de la geometria per trobar forats buits en diferents  agendes el més pròxims possibles en el temps.
Com a exemple,  imaginem que hem de donar cites a un pacient d’un hospital per fer un Preoperatori (proves que s’ha de realitzar abans d’una intervenció quirúrgica). Suposem, que un preoperatori consta d’una analítica, un electrocardiograma i una visita a l’anestesista. Per facilitar-li la feina al pacient, mirarem de donar-li les tres cites, el mateix dia i amb el temps d’estada al Hospital el més reduït possible.

## Fer correr el codi
Primer de tot desplegarem un contenidor docker amb SQL Server 2019 per linux

``docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=@thisIsAp0c" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest ``
