//using ecommerce.DAL;
//using ecommerce.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using System.Linq;
//using System;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace ecommerce.Interface
//{
//	public class CartRepository:ICartRepository
//	{
//		private readonly AppDbContext _db;
//		private readonly UserManager<IdentityUser> _userManager;
//		private readonly IHttpContextAccessor _httpContextAccessor;
//        public CartRepository(AppDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
//        {
//			_db = db;
//			_userManager = userManager;
//			httpContextAccessor = _httpContextAccessor;


//		}

//		public async Task<int> AddItem(int productId, int qty)
//		{
//			string userId = GetUserId();
//			using var transaction = _db.Database.BeginTransaction();
//			try
//			{
//				if (string.IsNullOrEmpty(userId))
//					throw new Exception("user is not logged-in");
//				var cart = await GetCart(userId);
//				if (cart is null)
//				{
//					cart = new ShoppingCart
//					{
//						UserId = userId
//					};
//					_db.ShoppingCarts.Add(cart);
//				}
//				_db.SaveChanges();
//				// cart detail section
//				var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
//				if (cartItem is not null)
//				{
//					cartItem.Quantity += qty;
//				}
//				else
//				{
//					var product = _db.Products.Find(productId);
//					cartItem = new CartDetail
//					{
//						ProductId = productId,
//						ShoppingCartId = cart.Id,
//						Quantity = qty,
//						/* = product.Price  */// it is a new line after update
//					};
//					_db.CartDetails.Add(cartItem);
//				}
//				_db.SaveChanges();
//				transaction.Commit();
//			}
//			catch (Exception ex)
//			{
//			}
//			var cartItemCount = await GetCartItemCount(userId);
//			return cartItemCount;
//		}



//		public async Task<ShoppingCart> GetUserCart()
//		{
//			var userId = GetUserId();
//			if (userId == null)
//				throw new Exception("Invalid userid");
//			var shoppingCart = await _db.ShoppingCarts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
//			return shoppingCart;

//		}
//		public async Task<ShoppingCart> GetCart(string userId)
//		{
//			var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
//			return cart;
//		}

//		public async Task<int> GetCartItemCount(string userId = "")
//		{
//			if (!string.IsNullOrEmpty(userId))
//			{
//				userId = GetUserId();
//			}
//			var data = await (from cart in _db.ShoppingCarts
//							  join cartDetail in _db.CartDetails
//							  on cart.Id equals cartDetail.ShoppingCartId
//							  select new { cartDetail.Id }
//						).ToListAsync();
//			return data.Count;
//		}

//		private string GetUserId()
//		{
//			var principal = _httpContextAccessor.HttpContext.User;
//			string userId = _userManager.GetUserId(principal);
//			return userId;
//		}


//	}



//}
