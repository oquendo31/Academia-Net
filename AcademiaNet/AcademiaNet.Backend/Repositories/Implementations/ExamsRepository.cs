using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Repositories.Implementations;

public class ExamsRepository : GenericRepository<Exam>, IExamsRepository
{
    private readonly DataContext _context;

    public ExamsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Exam>> GetComboAsync()
    {
        return await _context.Exams
            .OrderBy(x => x.Title)
            .ToListAsync();
    }

    //public override async Task<ActionResponse<IEnumerable<Exam>>> GetAsync()
    //{
    //    var Exams = await _context.Exams
    //        .Include(x => x.Title)
    //        .ToListAsync();
    //    return new ActionResponse<IEnumerable<Exam>>
    //    {
    //        WasSuccess = true,
    //        Result = Exams
    //    };
    //}

    public override async Task<ActionResponse<Exam>> GetAsync(int id)
    {
        var Exam = await _context.Exams
            .Include(x => x.Title)
            .FirstOrDefaultAsync(x => x.ExamID == id);
        if (Exam == null)
        {
            return new ActionResponse<Exam>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }
        return new ActionResponse<Exam>
        {
            WasSuccess = true,
            Result = Exam
        };
    }

    //public Task<ActionResponse<AcademicProgram>> UpdateAsync(ExamenDTO examenDTO)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<ActionResponse<Exam>> IExamsRepository.UpdateAsync(ExamenDTO examenDTO)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<ActionResponse<Exam>> AddAsync(ExamenDTO examenDTO)
    {
        var exam = new Exam
        {
            Title = examenDTO.Title,
            EnrollmentPeriodID = examenDTO.EnrollmentPeriodID,
            Questions = examenDTO.Questions.Select(q => new Question
            {
                Text = q.Text,
                Answers = q.Answers.Select(a => new Answer
                {
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList()
        };

        try
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();

            return new ActionResponse<Exam>
            {
                WasSuccess = true,
                Result = exam
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<Exam>
            {
                WasSuccess = false,
                Message = ex.Message
            };
        }
    }
}