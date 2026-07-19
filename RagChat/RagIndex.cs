using Comum;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;

namespace RagChat
{
    public static class RagIndex
    {
        private const string CollectionName = "nutricao";

        public static async Task<VectorStoreCollection<string, DocumentChunk>> BuildFromFolderAsync(string pastaConhecimento)
        {
            var vectorStore = new InMemoryVectorStore(new InMemoryVectorStoreOptions
            {
                EmbeddingGenerator = GeminiChat.BuildEmbeddingGenerator()
            });

            var collection = vectorStore.GetCollection<string, DocumentChunk>(CollectionName);
            await collection.EnsureCollectionExistsAsync();

            var chunks = CarregarChunks(pastaConhecimento);
            await collection.UpsertAsync(chunks);

            Console.WriteLine($"{chunks.Count} trechos indexados a partir de '{pastaConhecimento}'.");

            return collection;
        }

        private static List<DocumentChunk> CarregarChunks(string pastaConhecimento)
        {
            var chunks = new List<DocumentChunk>();

            foreach (var arquivo in Directory.EnumerateFiles(pastaConhecimento, "*.txt"))
            {
                var fonte = Path.GetFileNameWithoutExtension(arquivo);
                var texto = File.ReadAllText(arquivo);

                var trechos = TextChunker.Split(texto);

                for (var i = 0; i < trechos.Count; i++)
                {
                    chunks.Add(new DocumentChunk
                    {
                        Id = $"{fonte}-{i}",
                        Fonte = fonte,
                        Conteudo = trechos[i],
                        Embedding = trechos[i]
                    });
                }
            }

            return chunks;
        }
    }
}
