using System.IO;

namespace Dargon.League.WGEO {
   public interface IWGEOReaderFactory {
      WGEOFile Read(Stream stream);
   }
}
