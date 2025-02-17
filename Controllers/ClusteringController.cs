using Microsoft.AspNetCore.Mvc;
using FYPBackend.Services;
using System.Collections.Generic;

namespace FYPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusteringController : ControllerBase
    {
        private readonly ExcelReaderServices _clusteringService;

        public ClusteringController()
        {
            _clusteringService = new ExcelReaderServices();
        }

        [HttpGet("cluster")]
        public IActionResult GetClusters()
        {
            string filePath = "C:\\Users\\DELL\\source\\repos\\FYPBackend\\wwwroot\\uploads\\mm.xlsx"; // Adjust path
            List<ClusteredResult> clusters = _clusteringService.PerformClustering(filePath, 4);

            return Ok(clusters);
        }

    }
}
