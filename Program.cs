using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dargon.FileSystem;
using Dargon.IO;
using Dargon.IO.Drive;
using Dargon.ModelViewer;
using Dargon.Renderer;
using ItzWarty;
using ItzWarty.IO;

namespace Dargon.League.WGEO {
   class Program {
      static void Main(string[] args) {
         Console.WindowWidth = Console.BufferWidth = 200;
         Console.BufferHeight = Int16.MaxValue - 1;
         var readerFactory = new WGEOReaderFactory();
         var streamFactory = new StreamFactory();
         var driveNodeFactory = new DriveNodeFactory(streamFactory);
         var dargonNodeFactory = new DargonNodeFactory(driveNodeFactory);
         var fileSystemFactory = new FileSystemFactory(dargonNodeFactory);

         var fileSystem = fileSystemFactory.CreateFromDirectory(@"C:\DargonDumpNew\LEVELS");
         var root = fileSystem.AllocateRootHandle();

         IFileSystemHandle mapFolder;
         if (fileSystem.AllocateRelativeHandleFromPath(root, @"map11", out mapFolder) != IoResult.Success) {
            return;
         }

         WGEOFile wgeoFile;
         using (var fs = new FileStream(@"C:\DargonDumpNew\LEVELS\map11\Scene\room.wgeo", FileMode.Open)) {
            wgeoFile = readerFactory.ReadWGEOFile(fs);
         }

         var vertexBuffers = new List<float[]>();
         var indexBuffers = new List<ushort[]>();
         var textureDictionary = new Dictionary<string, int>();
         var textures = new List<byte[]>();

         wgeoFile.models.ForEach(model => {
            var newVertexBuffer = model.vertices.SelectMany(vert => new[] { vert.position.x, vert.position.y, vert.position.z, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, vert.uv.x, vert.uv.y }).ToArray();
            var newIndexBuffer = model.indices;

            vertexBuffers.Add(newVertexBuffer);
            indexBuffers.Add(newIndexBuffer);

            if (textureDictionary.ContainsKey(model.textureName))
               return;

            IFileSystemHandle textureHandle;
            if (fileSystem.AllocateRelativeHandleFromPath(mapFolder, "Scene/Textures/" + model.textureName.Trim('\0'), out textureHandle) != IoResult.Success) {
               throw new Exception("Couldn't find file");
            }

            byte[] data;
            if (fileSystem.ReadAllBytes(textureHandle, out data) != IoResult.Success) {
               throw new Exception("Couldn't read texture data");
            }

            textureDictionary.Add(model.textureName, textures.Count);
            textures.Add(data);
         });

         var models = Util.Generate(wgeoFile.models.Count, i => new Model {
            vertexBufferIndex = i,
            vertexOffset = 0,
            vertexCount = vertexBuffers[i].Length,
            indexBufferIndex = i,
            indexOffset = 0,
            indexCount = indexBuffers[i].Length,
            textureIndex = textureDictionary[wgeoFile.models[i].textureName],
            textureAddressMode = TextureAddressMode.Wrap
         }).ToList();

         //var thread = new Thread(() => {
            var modelViewer = new ModelViewerWindow();
            modelViewer.Initialize(1280, 720, (float)(Math.PI * 1.25), (float)(Math.PI * 0.25), 60000.0f, 0.0f, 0.0f, 0.0f, 12.0f, 10.0f);
            modelViewer.LoadModels(vertexBuffers, indexBuffers, textures, models);
            modelViewer.Start();
            modelViewer.Shutdown();
         //});

         //thread.Start();
         //thread.Join();
      }
   }
}
