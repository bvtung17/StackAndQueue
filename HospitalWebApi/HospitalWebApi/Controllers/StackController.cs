using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StackController : ControllerBase
    {
        private readonly ILogger<StackController> _logger;
        private static Stack<string> stackPatients = new Stack<string>();

        public StackController(ILogger<StackController> logger)
        {
            _logger = logger;

            if (!stackPatients.Any())
            {
                stackPatients.Push("BN1");
                stackPatients.Push("BN2");
                stackPatients.Push("BN3");
            }
        }

        // Stack : LIFO vào sau ra trước. (Bệnh nhân ưu tiên)
        // Queue : FIFO vào trước ra trước. (Bệnh nhận bình thường)

        [HttpGet(Name = "GetPatient")]
        public string Get()
        {
            return stackPatients.Pop();
        }

        [HttpPost("AddPriorityPatient")]
        public IActionResult AddPriority(string name)
        {
            // list ra : 3 2 1
            var a = new Stack<string>(); //4 1 2 3

            while (stackPatients.Count > 0)
            {
                a.Push(stackPatients.Pop());
            }

            a.Push(name);
            stackPatients = a;
            return Ok();
        }

        [HttpPost("AddPatient")]
        public IActionResult Add(string name)
        {
            var a = new Stack<string>();
            a.Push(name); // 4
            while (stackPatients.Count > 0)
            {
                a.Push(stackPatients.Pop());
            }

            stackPatients = a;

            return Ok();
        }
    }
}