
CREATE VIEW [dbo].[vw_report_gui_002]
AS
SELECT        i.BracnchNo, c.ChannelCategoryld, pb.Name, MONTH(i.CreatedDate) AS M_Month, YEAR(i.CreatedDate) AS Y_Year, COUNT(i.PwaIncidentID) AS All_Branch, SUM(CASE WHEN i.Status = 1 THEN 1 ELSE 0 END) AS Waiting, 
                         SUM(CASE WHEN (s.SLA * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) >= 0 THEN 1 ELSE 0 END) AS NotLate, SUM(CASE WHEN (s.SLA * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, 
                         ISNULL(i.EndSLADate, GETDATE())) < 0 THEN 1 ELSE 0 END) AS Late, SUM(CASE WHEN ((s.SLA) * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) < 0 AND ((s.SLA + 1) * 24 * 60) 
                         - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) >= 0 THEN 1 ELSE 0 END) AS Late1Day, SUM(CASE WHEN ((s.SLA + 1) * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) 
                         < 0 AND ((s.SLA + 2) * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) >= 0 THEN 1 ELSE 0 END) AS Late2Day, SUM(CASE WHEN ((s.SLA + 2) * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, 
                         ISNULL(i.EndSLADate, GETDATE())) < 0 AND ((s.SLA + 3) * 24 * 60) - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) >= 0 THEN 1 ELSE 0 END) AS Late3Day, SUM(CASE WHEN ((s.SLA + 3) * 24 * 60) 
                         - DATEDIFF(MINUTE, i.StartSLADate, ISNULL(i.EndSLADate, GETDATE())) < 0 THEN 1 ELSE 0 END) AS LateMore3Day, pb.Code
FROM            dbo.PwaIncident AS i INNER JOIN
                         dbo.PwaInformer AS f ON i.PwaIncidentID = f.PwaIncidentID INNER JOIN
                         dbo.SysChannel AS c ON f.InformChannelID = c.ChannelId INNER JOIN
                         dbo.SysRequestCategorySubject AS s ON i.CaseTitle = s.id INNER JOIN
                         dbo.SysBranch AS b ON i.BracnchNo = b.Code INNER JOIN
                         dbo.SysBranch AS pb ON b.Parent = pb.Code
WHERE        (i.Sla = 0) AND (i.CaseTitle <> 0) AND (i.Status = 1) AND (i.BracnchNo <> 1001) AND (i.StartSLADate IS NOT NULL)
GROUP BY i.BracnchNo, c.ChannelCategoryld, pb.Name, MONTH(i.CreatedDate), YEAR(i.CreatedDate), pb.Code