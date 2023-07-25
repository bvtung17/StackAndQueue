using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GateController : ControllerBase
    {
        private readonly ILogger<GateController> _logger;
        private static Stack<string> stack = new Stack<string>();
        private static Queue<string> queue = new Queue<string>();

        public GateController(ILogger<GateController> logger)
        {
            _logger = logger;

        }

        // Stack : LIFO vào sau ra trước. (Bệnh nhân ưu tiên)
        // Queue : FIFO vào trước ra trước. (Bệnh nhận bình thường)

        [HttpGet("Get")]
        public string Get()
        {
            if (stack.Any())
            {
                return stack.Pop();
            }

            if (!queue.Any())
            {
                return string.Empty;
            }

            return queue.Dequeue();
        }

        [HttpGet("AddValue")]
        public IActionResult AddValue()
        {
            queue.Enqueue("1");
            queue.Enqueue("2");
            queue.Enqueue("3");
            return Ok();
        }

        [HttpPost("AddPriorityPatient0")]
        public IActionResult AddPriority(string name)
        {
            stack.Push(name);
            return Ok();
        }

        [HttpPost("AddPatient0")]
        public IActionResult Add(string name)
        {
            queue.Enqueue(name);
            return Ok();
        }
    }
}