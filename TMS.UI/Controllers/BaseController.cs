using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace TMS.UI.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly T _service;

        public BaseController(T service)
        {
            _service = service;
        }
    }
}
