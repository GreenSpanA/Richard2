using Richard2.Models;
using System.Collections.Generic;

namespace Richard2.Models
{
    public class IndexViewModel
    {
        public IEnumerable<sampleMenu> sampleMenu { get; set; }
        public IEnumerable<currentFile> currentFile { get; set; }
    }
}
