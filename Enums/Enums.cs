using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enums
{
    public enum JobStatuses
    {
        JobCreated = 1,
        PickedUp = 2,
        OnSite = 3,
        Completed = 4
    }

    public enum EventTypes
    {
        JobCreated = 1,
        JobPickedUp = 2,
        JobTechnicianOnSite = 3,
        JobTechnicianDropped = 4,
        JobTechnicianCompleted = 5,
        QuoteCreated = 6,
        QuoteSurveyScheduled = 7,
        QuoteSurveyed = 8,
        QuoteApproved = 9,
        QuoteDeclined = 10,
        JobEtaUpdated = 11,
        JobRecalled = 12
    }

    public enum QuoteTypes
    {
        DesktopQuote = 1,
        SurveyQuote = 2
    }

    public enum QuoteStatuses
    {
        QuoteCreated = 1,
        SurveyScheduled = 2,
        AwaitingApproval = 3,
        QuoteApproved = 4,
        QuoteDeclined = 5
    }

    public enum ProductTypes
    {
        Product = 1,
        Part = 2,
        Labour = 3
    }
}
