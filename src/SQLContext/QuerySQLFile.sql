SELECT F.intworkitem        AS WorkItem, 
       A.intchangeset       AS Changesetid, 
       vchbranchs           AS Ambiente, 
       A.dtmstatuschangeset AS DataModificacao, 
       vchstatuschangeset   AS ChangesetStatus, 
       E.vchfilename        AS NomeArquivo 
FROM   tblsqlautomationchangesetcontrol A 
       INNER JOIN (SELECT intchangeset, 
                          Max(dtmstatuschangeset) as dtmStatusChangeset 
                   FROM   tblsqlautomationchangesetcontrol 
                   GROUP  BY intchangeset) B 
               ON A.dtmstatuschangeset = b.dtmstatuschangeset 
                  AND A.intchangeset = B.intchangeset 
       INNER JOIN tblsqlautomationstatuschangeset C 
               ON A.sinidsqlautomationstatuschangeset = 
                  C.sinidsqlautomationstatuschangeset 
       INNER JOIN tblsqlautomationbranchs D 
               ON A.sinidsqlautomationbranchs = D.sinidsqlautomationbranchs 
       INNER JOIN tblsqlautomationfilechangesetcontrol E 
               ON A.unsqlautomationchangesetcontrol = 
                  E.unsqlautomationchangesetcontrol 
       INNER JOIN tblsqlautomationworkitemchangeset F 
               ON A.unsqlautomationchangesetcontrol = 
                  F.unsqlautomationchangesetcontrol 
ORDER  BY datamodificacao DESC 