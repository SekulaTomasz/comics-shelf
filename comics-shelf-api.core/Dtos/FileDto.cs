using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class FileDto
    {
        public bool CanUserDownload { get; set; }
        public object File { get; set; }
    }
}
