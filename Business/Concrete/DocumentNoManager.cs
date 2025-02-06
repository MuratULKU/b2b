using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DocumentNoManager : IDocumentNoService
    {
        private readonly IUnitofWork _unitOfWork;

        public DocumentNoManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetDocNo(int doctype)
        {
            var docno = await _unitOfWork.DocumentNo.SingleOrDefaultAsync(x=>x.DocType == doctype);
            if (docno.Data == null)
            {
                await Insert(new DocumentNo() { DocType = doctype, DocNo = 1, Prefix = "" });
                docno = await _unitOfWork.DocumentNo.SingleOrDefaultAsync(x => x.DocType == doctype);
            }
            else
            {
               docno.Data.DocNo+=1;
               await Update(docno.Data);
            }
            await _unitOfWork.CommitAsync();
            return docno.Data.Prefix + docno.Data.DocNo;
        }

        public async Task<IResult> Insert(Entity.DocumentNo documentNo)
        {
            try
            {
                await _unitOfWork.DocumentNo.AddAsync(documentNo);
                return new Result(ResultStatus.Success, "DocumentNo inserted successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");

            }
        }

        public async Task<IResult> Update(Entity.DocumentNo documentNo)
        {
            try
            {
                await _unitOfWork.DocumentNo.UpdateAsync(documentNo);
                return new Result(ResultStatus.Success, "DocumentNo update successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"DocumentNo udate failed: {ex.Message}");

            }
        }
    }
}
