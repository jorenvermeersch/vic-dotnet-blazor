using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public static class TranslateEnums
{
    public static BackupFrequency TranslateBackupFrequency(string value)
    {
        switch (value)
        {
            case "Geen backup":
                return BackupFrequency.NoBackup;
            case "Dagelijks":
                return BackupFrequency.Daily;
            case "Wekelijks":
                return BackupFrequency.Weekly;
            case "Maandelijks":
                return BackupFrequency.Monthly;
            default:
                return BackupFrequency.NoBackup;
        }
    }
    public static Status TranslateStatus(string value)
    {
        switch (value)
        {
            case "In aanvraag":
                return Status.Requested;
            case "In verwerking":
                return Status.InProgress;
            case "Geconfigureerd":
                return Status.ReadyToDeploy;
            case "In gebruik":
                return Status.Deployed;
            default:
                return Status.Requested;
        }
    }
    public static Template TranslateTemplate(string value)
    {
        switch (value)
        {
            case "Geen template":
                return Template.NoTemplate;
            case "Artificiële intelligentie":
                return Template.AI;
            case "Web Server":
                return Template.WebServer;
            default:
                return Template.NoTemplate;
        }
    }
    public static Role TranslateRole(string value)
    {
        switch (value)
        {
            case "Waarnemer":
                return Role.Observer;
            case "Administrator":
                return Role.Admin;
            case "Master":
                return Role.Master;
            default:
                return Role.Observer;
        }
    }
}
