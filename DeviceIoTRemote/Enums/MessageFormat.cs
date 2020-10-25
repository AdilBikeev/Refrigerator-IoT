using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RemoteProvider.Enums
{
    /// <summary>
    /// Формат данных, передаваемых устройством.
    /// </summary>
    enum MessageFormat
    {
        [DefaultValue("application/json")]
        JSON,

        [DefaultValue("application/xml")]
        XML,

        [DefaultValue("text/html")]
        HTML
    }
}
