using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.UI.MVC.Models;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType1).Include(p => p.Vendor);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProductType = new SelectList(db.ProductTypes, "ProductType1", "TypeName");
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VendorName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductType,Description,Price,ProductImage,VendorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductType = new SelectList(db.ProductTypes, "ProductType1", "TypeName", product.ProductType);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VendorName", product.VendorID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductType = new SelectList(db.ProductTypes, "ProductType1", "TypeName", product.ProductType);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VendorName", product.VendorID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductType,Description,Price,ProductImage,VendorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductType = new SelectList(db.ProductTypes, "ProductType1", "TypeName", product.ProductType);
            ViewBag.VendorID = new SelectList(db.Vendors, "VendorID", "VendorName", product.VendorID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //POST: add to cart
        [HttpPost]
        public ActionResult AddToCart(int qty, int productID)
        {
            //Create the shell local shopping
            Dictionary<int, ShoppingCartViewModel> shoppingCart = null;

            //Check the global shopping cart
            if (Session["cart"] != null)
            {
                //if it has stuff in it, reassing to the local
                shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            }
            else
            {
                //create an empty local version
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
            }

            //get the product being displayed in the view
            Product product = db.Products.Where(x => x.ProductID == productID).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            else
            {
                //product is valid
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);
                //if the item is already in the cart just increase the qty
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else//add item to cart
                {
                    shoppingCart.Add(product.ProductID, item);
                }
                //update session cart with new item/qty
                Session["cart"] = shoppingCart;

                Session["confirm"] = string.Format($"{qty} {product.ProductName} " + $"{ ((qty > 1) ? "were" : "was")} added to your cart.");
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
