using System.IO;
using Dargon.Scene.Api;
using Dargon.Scene.Api.Util;

namespace Dargon.League.WGEO {
   public static class Extensions {
      public static Vector2 ReadVector2(this BinaryReader reader) {
         return new Vector2(reader.ReadSingle(), reader.ReadSingle());
      }

      public static Vector3 ReadVector3(this BinaryReader reader) {
         return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
      }

      public static Vector4 ReadVector4(this BinaryReader reader) {
         return new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
      }

      public static Vertex ReadWGEOVertex(this BinaryReader reader) {
         return new Vertex(reader.ReadVector3(), reader.ReadVector2());
      }
   }
}
