using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Logs.Shared;

namespace Web.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogger<LogsController> _logger;
        private readonly ILogRepository _logRepository;
        private readonly ILogTypeRepository _logTyperepository;
        private readonly IAuditRepository _auditRepository;

        public LogsController(
            ILogger<LogsController> logger,
            ILogRepository logRepository,
            ILogTypeRepository logTyperepository,
            IAuditRepository auditRepository)
        {
            _logger = logger;
            _logRepository = logRepository;
            _logTyperepository = logTyperepository;
            _auditRepository = auditRepository;
        }

        public async Task<IActionResult> Index(QueryWithTypeParameters queryParams)
        {
            if (queryParams.SortingParams == null || queryParams.SortingParams.Count == 0)
            {
                queryParams.SortingParams = new List<SortParam>()
                {
                    new SortParam{ OrderDescending = true, OrderProperty = "Id" }
                };
            }

            var rows = await _logRepository.GetPageAsync(queryParams);
            var types = await _logTyperepository.GetAllAsync();

            var data = new LogListViewModel(
                rows?.Entities?.Select(s => LogViewModel.FromLog(s))?.ToList(),
                types?.Select(s => LogTypeViewModel.FromLogType(s))?.ToList() ?? new List<LogTypeViewModel>());

            // Return the data with pagination and query keywords to keep the filters
            ViewBag.queryParams = queryParams;
            ViewBag.Count = rows?.Count ?? 0;

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                $"Visit Logs list page, fintering by: '{queryParams.Keywords}'. Pagination {queryParams.PageIndex}/{queryParams.PageSize}.",
                "Logs", null, Logs.BLL.Enums.AuditType.View));

            return View(data);
        }

        public async Task<IActionResult> New()
        {
            var types = await _logTyperepository.GetAllAsync();

            var data = new LogEditViewModel(
                null,
                types?.Select(s => LogTypeViewModel.FromLogType(s)).ToList());

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                $"The New Log page was viewed.",
                "Logs", null, Logs.BLL.Enums.AuditType.View));

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(LogEditRequest form)
        {
            var log = await _logRepository.AddAsync(form.ToLog());

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                $"New Log record was created, ID: '{log.Id}'.",
                "Logs", null, Logs.BLL.Enums.AuditType.View));

            return RedirectPermanent("/logs");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return RedirectPermanent("/logs");

            var types = await _logTyperepository.GetAllAsync();
            Log log = await _logRepository.GetByIdAsync(id);

            var data = new LogEditViewModel(
                LogViewModel.FromLog(log),
                types.Select(s => LogTypeViewModel.FromLogType(s)).ToList());

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                $"The Edit Log page with ID: {id}, was viewed.",
                "Logs", null, Logs.BLL.Enums.AuditType.View));

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LogEditRequest form)
        {
            await _logRepository.UpdateAsync(form.ToLog());

            // Store the Audit track
            await _auditRepository.AddAsync(AuditModel.ToAudit(
                $"The Log record was updated, ID: '{form.Id}'.",
                "Logs", null, Logs.BLL.Enums.AuditType.View));

            return RedirectPermanent("/logs");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
