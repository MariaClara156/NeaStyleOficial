using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Repositories; // Você vai precisar do seu Repository aqui!

namespace NeaStyleOficial.Services
{
    public class CarrinhoService
    {
        // Injeção de dependência do repositório de carrinho
        private readonly CarrinhoRepository _carrinhoRepo;
        // O construtor recebe o repositório para poder acessar os dados do carrinho
        public CarrinhoService(CarrinhoRepository carrinhoRepo)
        {
            _carrinhoRepo = carrinhoRepo;
        }

        // Método para adicionar um produto ao carrinho de um cliente
        public void AdicionarProduto(long clienteId, Produto produto, int quantidade)
        {
            if (produto == null) throw new Exception("Produto inválido!");

            // 1. Busca o carrinho atual do cliente no banco
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);

            // 2. Se o carrinho não existe, cria um novo
            if (carrinho == null)
            {
                carrinho = new Carrinho(clienteId);
                _carrinhoRepo.Criar(carrinho);
            }

            // 3. Regra de Negócio: Limite de itens
            if (carrinho.Itens.Sum(i => i.Quantidade) + quantidade > 50)
                throw new Exception("Limite de 50 itens no carrinho atingido!");

            // 3. Verifica se o produto já existe no carrinho para apenas somar a quantidade
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produto.ProdutoId);

            if (itemExistente != null) {
                itemExistente.Quantidade += quantidade;
            } else {
                carrinho.Itens.Add(new ItemConjunto { 
                    ProdutoId = produto.ProdutoId, 
                    Quantidade = quantidade 
                });
            }

            _carrinhoRepo.Atualizar(carrinho);
        }

        public void RemoverProduto(long clienteId, long produtoId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null) throw new Exception("Carrinho não encontrado!");

            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item == null) throw new Exception("Produto não encontrado no carrinho!");

            carrinho.Itens.Remove(item);
            _carrinhoRepo.Atualizar(carrinho);
        }

        public void Atualizar(Carrinho carrinho)
        {
            var carrinhoExistente = _carrinhoRepo.BuscarPorClienteId(carrinho.ClienteId);
            if (carrinhoExistente == null) throw new Exception("Carrinho não encontrado!");
            /* lógica pra atualizar */
            _carrinhoRepo.Atualizar(carrinho);
            
        }

        public decimal CalcularTotal(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null || !carrinho.Itens.Any())
            {
                return 0;
            }
            // Aqui multiplicamos o preço do produto pela quantidade de cada item
            return carrinho.Itens.Sum(i => i.Produto.Preco * i.Quantidade);
        }

        public List<ItemConjunto> ObterItens(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null) return new List<ItemConjunto>();
            return carrinho.Itens;
        }
        public void LimparCarrinho(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null) throw new Exception("Carrinho não encontrado!");
            carrinho.Itens.Clear();
            _carrinhoRepo.Atualizar(carrinho);
        }
    }
}