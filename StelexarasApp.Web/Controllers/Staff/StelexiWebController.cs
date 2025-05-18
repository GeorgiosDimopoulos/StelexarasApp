using Microsoft.AspNetCore.Mvc;
using StelexarasApp.Library.Dtos.People.Staff;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Services.Interfaces.People;

namespace StelexarasApp.Web.Controllers.WebControllers.Staff;

[Route("StaffWeb")]
public class StelexiWebController : Controller
{
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> _staffService;
    private readonly ILogger<StelexiWebController> _logger;

    public StelexiWebController(IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse> staffService, ILogger<StelexiWebController> logger)
    {
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        _logger = logger;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var staffList = await _staffService.GetStelexi(Thesi.None, string.Empty, new());
            if (staffList == null || !staffList.Any())
            {
                _logger.LogWarning("No staff members found.");
                return NotFound("No staff data available.");
            }
            return View(staffList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the staff list.");
            ViewData ["ErrorMessage"] = "An error occurred while fetching the staff list. Please try again later.";
            return View("Error");
        }
    }

    // GET: StaffWeb/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: StaffWeb/Create
    [HttpPost("CreateOmadarxis")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOmadarxis([Bind("FullName,Position,Phone")] CreateStelexosRequest createStelexosRequest)
    {
        if (!ModelState.IsValid)
            return View(createStelexosRequest);

        try
        {
            var result = await _staffService.CreateStelexos(createStelexosRequest, Thesi.Omadarxis);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create staff member.");
            return View(createStelexosRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new staff member.");
            return View("Error", new { message = "An error occurred while creating a new staff member." });
        }
    }

    // POST: StaffWeb/Create
    [HttpPost("CreateKoinotarxis")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateKoinotarxis([Bind("FullName,Position,Phone")] CreateStelexosRequest createKoinotarxisRequest)
    {
        if (!ModelState.IsValid)
            return View(createKoinotarxisRequest);

        try
        {
            var result = await _staffService.CreateStelexos(createKoinotarxisRequest, Thesi.Koinotarxis);
            if (result)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create staff member.");
            return View(createKoinotarxisRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new staff member.");
            return View("Error", new { message = "An error occurred while creating a new staff member." });
        }
    }

    // GET: StaffWeb/Edit/5
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id, [FromQuery] Thesi? thesi)
    {
        if (id <= 0)
            return BadRequest("Invalid staff ID.");

        try
        {
            var staffMember = await _staffService.GetStelexosById(id, new());
            if (staffMember == null)
            {
                _logger.LogWarning($"Staff member with ID {id} not found.");
                return NotFound("Staff member not found.");
            }
            return View(staffMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching staff member with ID {id} for editing.");
            return View("Error", new { message = $"An error occurred while fetching staff member with ID {id} for editing." });
        }
    }

    // GET: StaffWeb/Delete/5
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid staff ID.");

        try
        {
            var staffMember = await _staffService.DeleteStelexos(new DeleteStelexosRequest() { Id = id });
            if (staffMember == null)
            {
                _logger.LogWarning($"Staff member with ID {id} not found.");
                return NotFound("Staff member not found.");
            }
            return View(staffMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching staff member with ID {id} for deletion.");
            return View("Error", new { message = $"An error occurred while fetching staff member with ID {id} for deletion." });
        }
    }
}
