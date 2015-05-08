using System;
using System.Collections.Generic;
using System.IO;
using ItzWarty;
using System.Linq;

namespace Dargon.League.WGEO {
   class WGEOReaderFactory : IWGEOReaderFactory {
      public WGEOFile ReadWGEOFile(Stream stream) {
         using (var reader = new BinaryReader(stream)) {
            var file = new WGEOFile();

            var magic = reader.ReadUInt32();
            if (magic != 1329940311) { 
               throw new Exception("Wrong magic");
            }

            var version = reader.ReadUInt32();
            var numberOfModels = reader.ReadUInt32();
            var unknown1 = reader.ReadUInt32();

            // Start of data sections


            for (var modelNumber = 0; modelNumber < numberOfModels; ++modelNumber) {
               var model = new WGEOModel(); 

               // Console.WriteLine('\n' + reader.BaseStream.Position.ToString("X"));

               model.textureName = new BinaryReader(new MemoryStream(reader.ReadBytes(260))).ReadNullTerminatedString();
               var materialName = new BinaryReader(new MemoryStream(reader.ReadBytes(64))).ReadNullTerminatedString();

               //Console.WriteLine("Texture name: {0}", textureName);
               //Console.WriteLine("Material name: {0}", materialName);

               var boundingSphere = reader.ReadFloat4();
               var AABBMin = reader.ReadFloat3();
               var AABBMax = reader.ReadFloat3();

               var vertexCount = reader.ReadInt32();
               var indexCount = reader.ReadInt32();

               //Console.WriteLine("\{vertexCount} Vertices found");
               //Console.WriteLine("\{indexCount} Indices found");

               model.vertices = Util.Generate(vertexCount, i => reader.ReadWGEOVertex());
               model.indices = Util.Generate(indexCount, i => reader.ReadUInt16());

               //Console.WriteLine(vertices[0].position);

               //var min = new Float3(vertices[0].position);
               //var max = new Float3(vertices[0].position);

               //vertices.ForEach(vertex => {
               //   min.x = Math.Min(min.x, vertex.position.x);
               //   min.y = Math.Min(min.y, vertex.position.y);
               //   min.z = Math.Min(min.z, vertex.position.z);
               //});
               //vertices.ForEach(vertex => {
               //   max.x = Math.Max(max.x, vertex.position.x);
               //   max.y = Math.Max(max.y, vertex.position.y);
               //   max.z = Math.Max(max.z, vertex.position.z);
               //});

               //Console.WriteLine("Min: {0}", min);
               //Console.WriteLine("Max: {0}", max);

               file.models.Add(model);
            }

            return file;
         }
      }
   }
}
