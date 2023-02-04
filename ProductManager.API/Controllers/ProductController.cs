using JsonPatch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ProductManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        //Product List
        public static List<Product> _products = new List<Product>()
        {
             new Product(1, "Iphone7", "Phone", "New", 400, 15, "Apple"),
             new Product(2, "Iphone8", "Phone", "New", 420, 15, "Apple"),
             new Product(3, "Iphone9", "Phone", "New", 430, 15, "Apple"),
             new Product(4, "IphoneX", "Phone", "New", 440, 15, "Apple"),
             new Product(5, "Iphone10", "Phone", "New", 460, 15, "Apple"),
             new Product(6, "Dellİ7", "Pc", "New", 960, 15, "Dell"),
             new Product(7, "Excaliburİ5", "Pc", "New", 460, 15, "Casper"),
             new Product(8, "Asusİ7", "Pc", "Used", 560, 15, "Asus"),
             new Product(9, "Lenovoİ3", "Pc", "New", 760, 15, "Lenovo"),
             new Product(11, "IPhone14", "Phone", "New", 1200, 15, "Apple")
        };


        /// <summary>
        /// Insert Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("/insert")]
        public ActionResult<Product> Insert([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }


                var isExist = _products.Find(x => x.Id == product.Id);

                if (isExist != null)
                {
                    ModelState.AddModelError("Id", "Product already in use");
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid)
                {
                    _products.Add(product);
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("/products")]
        public IActionResult GetAll()
        {
            try
            {
                if (_products.Count == 0)
                {
                    ModelState.AddModelError("Error", "List is Empty");
                    return BadRequest(ModelState);
                }
                return Ok(_products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest();
                }
                Product product = _products.Find(x => x.Id == id);


                if (ModelState.IsValid)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }

        }


        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }


                var product = _products.Find(x => x.Id == id);

                if (product == null)
                {
                    ModelState.AddModelError("Id", "Can not Found Product");
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid)
                {
                    _products.Remove(product);
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }


        }


        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("/update")]
        public ActionResult<Product> Update([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }


                Product updatedProduct = _products.Find(x => x.Id == product.Id);

                if (updatedProduct == null)
                {
                    ModelState.AddModelError("Id", "Product not found");
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid)
                {
                    updatedProduct.name = product.name;
                    updatedProduct.category = product.category;
                    updatedProduct.description = product.description;
                    updatedProduct.price = product.price;
                    updatedProduct.quantity = product.quantity;
                    updatedProduct.suppliers = product.suppliers;
                }
                return Ok(updatedProduct);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }
        }


        /// <summary>
        /// Partial Update
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPatch("/update")]
        public ActionResult<Product> PartialUpdate([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }


                Product updatedProduct = _products.Find(x => x.Id == product.Id);

                if (updatedProduct == null)
                {
                    ModelState.AddModelError("Id", "Product not found");
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    updatedProduct.name = product.name;
                    updatedProduct.price = product.price;
                }


                return Ok(updatedProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }
        }

        /// <summary>
        /// get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("/products/{name}")]
        public IActionResult getProductByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest();
                }

                var products = _products.Where(x => x.name == name);

                if (ModelState.IsValid)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }


        }

        /// <summary>
        /// Order by price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet("/products/list/{price}")]
        public IActionResult ListProducts(int price)
        {

            try
            {
                if (price <= 0)
                {
                    return BadRequest();
                }

                var products = _products.Where(x => x.price > price)
                .Select(s =>
                new
                {
                    s.name,
                    s.price,
                    s.description,
                    s.category
                }
                    ).OrderByDescending(x => x.price);



                if (ModelState.IsValid)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Product list");
            }

        }

    }
}



