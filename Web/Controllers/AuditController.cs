using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logs.BLL.Interfaces;
using Logs.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class AuditController : Controller
    {
        private readonly ILogger<AuditController> _logger;
        private readonly IAuditRepository _auditRepository;

        public AuditController(
            ILogger<AuditController> logger,
            ILogRepository logRepository,
            ILogTypeRepository logTyperepository,
            IAuditRepository auditRepository)
        {
            _logger = logger;
            _auditRepository = auditRepository;
        }

        public async Task<IActionResult> Index(QueryWithTypeParameters queryParams)
        {
            if (queryParams.SortingParams == null || queryParams.SortingParams.Count == 0)
            {
                queryParams.SortingParams = new System.Collections.Generic.List<SortParam>()
                {
                    new SortParam{ OrderDescending = true, OrderProperty = "Id" }
                };
            }

            var rows = await _auditRepository.GetPageAsync(queryParams);

            var data = rows?.Entities?.Select(s => AuditViewModel.FromAudit(s)).ToList();

            // Return the data with pagination and query keywords to keep the filters
            ViewBag.queryParams = queryParams;
            ViewBag.Count = rows?.Count ?? 0;

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                @"The Audit list page was viewed.",
                "Audits", null, Logs.BLL.Enums.AuditType.View));

            return View(data);
        }
    }
}
