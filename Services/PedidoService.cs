using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Repositories;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly CarrinhoRepository _carrinhoRepo;

        public PedidoService(PedidoRepository pedidoRepo, CarrinhoRepository carrinhoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _carrinhoRepo = carrinhoRepo;
        }

        public void RealizarPedido(long clienteId)
        {
                // 1. Busca o carrinho usando o Repository
                var carrinho = _carrinhoRepo.BuscarPorClienteId(clienteId);
                
                if (!carrinho.Itens.Any())
                    throw new Exception("Não é possível fechar um pedido com o carrinho vazio!");

                // 2. Transforma Itens do Carrinho em Itens do Pedido
                var novoPedido = new Pedido {
                    ClienteId = clienteId,
                    DataPedido = DateTime.Now,
                    Status = StatusPedido.Pendente,
                    ValorTotal = carrinho.Itens.Sum(i => i.Produto.Preco * i.Quantidade)
                };

                // 3. Salva o pedido e limpa o carrinho
                _pedidoRepo.Criar(novoPedido);
                _carrinhoRepo.Limpar(carrinho.ConjuntoProdutoId);
        }
        public List<Pedido> VerPedidos(long clienteId)
        {
            return _pedidoRepo.BuscarPorClienteId(clienteId);
        }
        

        public Pedido VerDetalhesPedido(long PedidoId)
        {
            var pedido = _pedidoRepo.BuscarPorId(PedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado!");
            return pedido;
        }

        public void CancelarPedido(long PedidoId)
        {
            var pedido = _pedidoRepo.BuscarPorId(PedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado!");
            if (pedido.Status != StatusPedido.Pendente)
                throw new Exception("Somente pedidos pendentes podem ser cancelados!");
    
            pedido.Status = StatusPedido.Cancelado;
            // CancelarPedido
            _pedidoRepo.Atualizar(pedido.PedidoId, StatusPedido.Cancelado);
        }
    }
}