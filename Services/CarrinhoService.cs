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
            if (produtoVariacao == null)
                throw new Exception("Produto inválido!");

            // Busca o carrinho atual do cliente; se não existir, cria um novo
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            if (carrinho == null)
            {
                carrinho = new Carrinho(clienteId);
                _carrinhoRepo.Criar(carrinho);
                carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId); // busca de novo com Include
            }

            // Regra de negócio: limite de 50 itens no carrinho
            if (carrinho.Itens.Sum(i => i.Quantidade) + quantidade > 50)
                throw new Exception("Limite de 50 itens no carrinho atingido!");

            // Se o produto já existe no carrinho, apenas soma a quantidade
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoVariacaoId == produtoVariacao.ProdutoVariacaoId);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                var novoItem = new ItemConjunto(produtoVariacao.ProdutoVariacaoId, quantidade)
                {
                    CarrinhoId = carrinho.ConjuntoProdutoId
                };
                carrinho.Itens.Add(novoItem);
            }

            _carrinhoRepo.Atualizar(carrinho);
        }

        public void RemoverProduto(long clienteId, long produtoVariacaoId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);

            if (carrinho == null)
                throw new Exception("Carrinho não encontrado!");

            if (carrinho.Finalizado)
                throw new Exception("Não é possível remover itens de um carrinho finalizado!");

            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoVariacaoId == produtoVariacaoId);

            if (item == null)
                throw new Exception("Produto não encontrado no carrinho!");

            carrinho.Itens.Remove(item);
            _carrinhoRepo.Atualizar(carrinho);
        }

        public void Atualizar(Carrinho carrinho)
        {
            if (carrinho.Finalizado)
                throw new Exception("Não é possível atualizar um carrinho finalizado!");

            var carrinhoExistente = _carrinhoRepo.BuscarPorClienteId(carrinho.ClienteId);

            if (carrinhoExistente == null)
                throw new Exception("Carrinho não encontrado!");

            _carrinhoRepo.Atualizar(carrinho);
        }

        public decimal CalcularTotal(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
            return carrinho.Itens.Sum(i => i.Quantidade * i.ProdutoVariacao.Preco);
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

            if (carrinho == null)
                throw new Exception("Carrinho não encontrado!");

            if (carrinho.Finalizado)
                throw new Exception("Não é possível remover itens de um carrinho finalizado!");

            carrinho.Itens.Clear();
            _carrinhoRepo.Atualizar(carrinho);
        }
    }
}