using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Repositories;

namespace NeaStyleOficial.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository    _pedidoRepo;
        private readonly CarrinhoRepository  _carrinhoRepo;
        private readonly ProdutoRepository   _produtoRepo;

        public PedidoService(PedidoRepository pedidoRepo, CarrinhoRepository carrinhoRepo, ProdutoRepository produtoRepo)
        {
            _pedidoRepo   = pedidoRepo;
            _carrinhoRepo = carrinhoRepo;
            _produtoRepo  = produtoRepo;
        }

        public long RealizarPedido(long clienteId)
        {
            var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);

            if (carrinho == null)       throw new Exception("Carrinho não encontrado!");
            if (!carrinho.Itens.Any())  throw new Exception("Carrinho vazio!");

            foreach (var item in carrinho.Itens)
            {
                var variacao = _produtoRepo.BuscarVariacaoPorId(item.ProdutoVariacaoId);
                if (variacao == null)
                    throw new Exception($"Produto '{item.ProdutoVariacao.Produto.Nome}' não encontrado!");
                if (variacao.Estoque < item.Quantidade)
                    throw new Exception($"Estoque insuficiente para '{item.ProdutoVariacao.Produto.Nome}'. Disponível: {variacao.Estoque}");
            }

            var novoPedido = new Pedido
            {
                ClienteId  = clienteId,
                DataPedido = DateTime.Now,
                Status     = StatusPedido.Pendente,
                ValorTotal = carrinho.Itens.Sum(i => i.ProdutoVariacao.Preco * i.Quantidade)
            };

            foreach (var item in carrinho.Itens)
            {
                novoPedido.Itens.Add(new ItemPedido(
                    pedidoId:          0,
                    produtoVariacaoId: item.ProdutoVariacaoId,
                    nomeProduto:       item.ProdutoVariacao.Produto.Nome,
                    quantidade:        item.Quantidade,
                    precoUnitario:     item.ProdutoVariacao.Preco,
                    imagemUrl:         item.ProdutoVariacao.ImagemUrl
                ));

                // ← ADICIONA: desconta o estoque
                var variacao = _produtoRepo.BuscarVariacaoPorId(item.ProdutoVariacaoId);
                variacao.Estoque -= item.Quantidade;
                _produtoRepo.AtualizarVariacao(variacao);
            }

            _pedidoRepo.Criar(novoPedido);
            _carrinhoRepo.Limpar(carrinho.ConjuntoProdutoId);
            return novoPedido.PedidoId;
        
        }

        public List<Pedido> VerPedidos(long clienteId)      => _pedidoRepo.BuscarPorClienteId(clienteId);
        public List<Pedido> VerTodosPedidos()               => _pedidoRepo.BuscarTodos();

        public Pedido VerDetalhesPedido(long pedidoId)
        {
            var pedido = _pedidoRepo.BuscarPorId(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado!");
            return pedido;
        }

        public void AlterarStatus(long pedidoId, StatusPedido novoStatus)
        {
            var pedido = _pedidoRepo.BuscarPorId(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado!");
            _pedidoRepo.Atualizar(pedidoId, novoStatus);
        }

        public void CancelarPedido(long pedidoId)
        {
            var pedido = _pedidoRepo.BuscarPorId(pedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado!");

            if (pedido.Status != StatusPedido.Pendente)
                throw new Exception("Somente pedidos pendentes podem ser cancelados!");

            _pedidoRepo.Atualizar(pedido.PedidoId, StatusPedido.Cancelado);
        }
    }
}