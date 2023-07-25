using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly ILogger<QueueController> _logger;
        private static Queue<string> queuePatients = new Queue<string>();

        public QueueController(ILogger<QueueController> logger)
        {
            _logger = logger;

            if (!queuePatients.Any())
            {
                queuePatients.Enqueue("BN1");
                queuePatients.Enqueue("BN2");
                queuePatients.Enqueue("BN3");
            }
        }

        // Stack : LIFO vào sau ra trước. (Bệnh nhân ưu tiên)
        // Queue : FIFO vào trước ra trước. (Bệnh nhận bình thường)

        [HttpGet(Name = "GetPatient2")]
        public string Get()
        {
            return queuePatients.Dequeue();
        }

        [HttpPost("AddPriorityPatient2")]
        public IActionResult AddPriority(string name)
        {
            var a = new Queue<string>();
            a.Enqueue(name); // 4
            var count = queuePatients.Count();
            for (int i = 0; i < count; i++)
            {
                a.Enqueue(queuePatients.Dequeue());
            }

            queuePatients = a;
            return Ok();
        }

        [HttpPost("AddPatient2")]
        public IActionResult Add(string name)
        {
            queuePatients.Enqueue(name);
            return Ok();
        }
    }
}