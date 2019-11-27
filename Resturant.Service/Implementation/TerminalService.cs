using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Model;
using Resturant.Repository;
using Resturant.Repository.Implementation;
using Resturant.Repository.Interface;
//using TMDoyle.Service.Interfaces;
using Resturant.Service.RequestFormatters;
using Resturant.Service.ResponseFormatters;

namespace Resturant.Service.Implementation
{
    #region Interface
    public interface ITerminalService
    {
        int CreateTerminal(TerminalDTO terminalDTO);
        List<TerminalDTO> GetAllTerminals();
        TerminalDTO GetTerminalById(int id);
        bool UpdateTerminal(TerminalDTO terminalDTO);
        bool DeleteTerminal(int id);
    }
    #endregion

    #region Implementation
    public class TerminalService : ITerminalService
    {
        private IUnitOfWork _unitOfWork;
        public TerminalService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public List<TerminalDTO> GetAllTerminals()
        {
            List<Terminal> terminals = _unitOfWork.TerminalRepository.All().ToList();
            return terminals.Convert();
        }

        public int CreateTerminal(TerminalDTO terminalDTO)
        {
            Terminal terminal = terminalDTO.Convert();
            _unitOfWork.TerminalRepository.Insert(terminal);
            return _unitOfWork.Save();
        }

        public TerminalDTO GetTerminalById(int id)
        {
            Terminal terminal = _unitOfWork.TerminalRepository.GetById(id);
            TerminalDTO terminalDTO = terminal.Convert();
            return terminalDTO;
        }

        public Boolean UpdateTerminal(TerminalDTO terminalDTO)
        {
            Terminal _editingTerminal = _unitOfWork.TerminalRepository.GetById(terminalDTO.Id);
            _editingTerminal.Code = terminalDTO.Code;
            _editingTerminal.CompanyId = terminalDTO.CompanyId;
            _editingTerminal.Description = terminalDTO.Description;
            _editingTerminal.ModifiedById = terminalDTO.ModifiedById;
            _editingTerminal.ModifiedOn = System.DateTime.Now;
            _editingTerminal.TBTRNFileNumber = terminalDTO.TBTRNFileNumber;
            
            //_editingTerminal = terminalDTO.Convert();
            _unitOfWork.TerminalRepository.Update(_editingTerminal);
            return true;

        }

        public bool DeleteTerminal(int id)
        {
            _unitOfWork.TerminalRepository.Delete(id);
            return true;
        }
    }

    #endregion
}
