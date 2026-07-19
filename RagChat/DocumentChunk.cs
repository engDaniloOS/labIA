using Comum;
using Microsoft.Extensions.VectorData;

namespace RagChat
{
    public class DocumentChunk
    {
        [VectorStoreKey]
        public string Id { get; set; } = "";

        [VectorStoreData]
        public string Fonte { get; set; } = "";

        [VectorStoreData]
        public string Conteudo { get; set; } = "";

        [VectorStoreVector(GeminiChat.EmbeddingDimensions)]
        public string Embedding { get; set; } = "";
    }
}
