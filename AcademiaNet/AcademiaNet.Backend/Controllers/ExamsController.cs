using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaNet.Backend.Controllers;

public class ExamsController : GenericController<Exam>
{
    private readonly IExamsUnitOfWorks _ExamsUnitOfWorks;

    public ExamsController(IGenericUnitOfWork<Exam> unitOfWork, IExamsUnitOfWorks ExamsUnitOfWorks) : base(unitOfWork)
    {
        _ExamsUnitOfWorks = ExamsUnitOfWorks;
    }

    [HttpPost("PostExam")]
    public async Task<IActionResult> PostExam([FromBody] ExamenDTO examDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Si el modelo no es válido, retornamos un error 400.
        }

        // Llamamos al Unit of Work para agregar el examen
        var response = await _ExamsUnitOfWorks.AddAsync(examDto);

        // Verificamos si la operación fue exitosa
        if (!response.WasSuccess)

        {
            return StatusCode(500, response.Message); // Si la operación no fue exitosa, retornamos un error 500.
        }

        // Chequeamos explícitamente si `response.Result` es nulo
        if (response.Result == null)
        {
            return StatusCode(500, "Error: El examen no se ha creado correctamente."); // O cualquier otro mensaje que prefieras
        }

        // Aquí estamos controlando la respuesta en el controlador.
        // Si la operación fue exitosa, devolvemos el examen creado.
        return CreatedAtAction(nameof(GetExam), new { id = response.Result.ExamID }, response.Result);
    }

    [HttpGet("exams/{id}")]  // Ruta específica para obtener un examen por ID
    public async Task<IActionResult> GetExam(int id)
    {
        var exam = await _ExamsUnitOfWorks.GetAsync(id);  // Obtener el examen por ID
        if (exam == null)
        {
            return NotFound();  // Si no se encuentra el examen, retornamos un 404
        }
        return Ok(exam);  // Retornamos el examen encontrado
    }

    /// <summary>
    /// Get All Exams
    /// </summary>
    /// <returns></returns>

    //[HttpGet("exams")]
    //public async Task<IActionResult> GetAllExams()
    //{
    //    var exams = await _ExamsUnitOfWorks.GetAllAsync();
    //    return Ok(exams);  // Retornamos la lista de exámenes
    //}

    /// <summary>
    /// Update Exam
    /// </summary>
    /// <param name="id"></param>
    /// <param name="examDto"></param>
    /// <returns></returns>
    //[HttpPut("exams/{id}")]
    //public async Task<IActionResult> UpdateExam(int id, [FromBody] ExamenDTO examDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);  // Si el modelo no es válido, retornamos un error 400
    //    }

    //    var response = await _ExamsUnitOfWorks.UpdateAsync(id, examDto);  // Actualizamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Retornamos un error 500 si no fue exitoso
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    //[HttpDelete("exams/{id}")]
    //public async Task<IActionResult> DeleteExam(int id)
    //{
    //    var response = await _ExamsUnitOfWorks.DeleteAsync(id);  // Eliminamos el examen
    //    if (!response.WasSuccess)
    //    {
    //        return StatusCode(500, response.Message);  // Si hay un error, retornamos un 500
    //    }

    //    return NoContent();  // Si fue exitoso, retornamos 204 No Content
    //}
}