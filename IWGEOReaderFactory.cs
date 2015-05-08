using System.IO;

namespace Dargon.League.WGEO {
   interface IWGEOReaderFactory {
      WGEOFile ReadWGEOFile(Stream stream);
   }
}
