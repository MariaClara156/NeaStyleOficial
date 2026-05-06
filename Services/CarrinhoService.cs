using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Repositories;
namespace NeaStyleOficial.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository _carrinhoRepo;
        public CarrinhoService(CarrinhoRepository carrinhoRepo)
        {
            _carrinhoRepo = carrinhoRepo;
        }

        public void AdicionarProduto(long clienteId, ProdutoVariacao produtoVariacao, int quantidade)
        {
            if (produtoVariacao == null) throw new Exception("Produto inválido!");
            // Busca o carrinho atual do cliente no banco
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            // Se o carrinho não existe, cria um novo
            if (carrinho == null)
            {
                carrinho = new Carrinho(clienteId);
                _carrinhoRepo.Criar(carrinho);
            }
            // Regra de Negócio: Limite de itens
            if (carrinho.Itens.Sum(i => i.Quantidade) + quantidade > 50)
                throw new Exception("Limite de 50 itens no carrinho atingido!");
            // Verifica se o produto já existe no carrinho para apenas somar a quantidade
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoVariacaoId == produtoVariacao.ProdutoVariacaoId);
            if (itemExistente != null){
                itemExistente.Quantidade += quantidade;
            } 
            else{
                carrinho.Itens.Add(produtoVariacao.ProdutoVariacaoId, quantidade);
            }
            _carrinhoRepo.Atualizar(carrinho);
        }

        public void RemoverProduto(long clienteId, long produtoVariacaoId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if(carrinho.Finalizado)
                throw new Exception("Não é possível remover itens de um carrinho finalizado!");
            if (carrinho == null) throw new Exception("Carrinho não encontrado!");

            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoVariacaoId == produtoVariacaoId);
            if (item == null) throw new Exception("Produto não encontrado no carrinho!");

            

            carrinho.Itens.Remove(item);
            _carrinhoRepo.Atualizar(carrinho);
        }

        public void Atualizar(Carrinho carrinho)
        {
            var carrinhoExistente = _carrinhoRepo.BuscarPorClienteId(carrinho.ClienteId);
            if(carrinho.Finalizado)
                throw new Exception("Não é possível atualizar um carrinho finalizado!");
            if (carrinhoExistente == null) throw new Exception("Carrinho não encontrado!");
            
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
            return carrinho.Itens.Sum(i => i.ProdutoVariacao.Preco * i.Quantidade);
        }

        public List<ItemConjunto> ObterItens(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null) return new List<ItemConjunto>();
            return carrinho.Itens.ToList();
        }
        public void LimparCarrinho(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if(carrinho.Finalizado)
                throw new Exception("Não é possível limpar um carrinho finalizado!");
            if (carrinho == null) throw new Exception("Carrinho não encontrado!");
            
            carrinho.Itens.Clear();
            _carrinhoRepo.Atualizar(carrinho);
        }
    }
}