using AutoMapper;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Services.Services.IServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.ViewModels.TeamsViewModels;

public class SkiniViewModel : INotifyPropertyChanged
{
    private readonly IPaidiaService _paidiaService;
    private readonly IMapper mapper;

    public Skini Skini { get; set; }

    public SkiniViewModel(SkiniDto skini, IPaidiaService paidiaService)
    {
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SkiniDto, Skini>();
            cfg.CreateMap<PaidiDto, Paidi>();
        }).CreateMapper();

        this.Skini = mapper.Map<Skini>(skini);
        _paidiaService = paidiaService;
    }

    public async Task<bool> AddPaidiAsync(PaidiDto paidiDto)
    {
        if (string.IsNullOrEmpty(paidiDto.FullName) || string.IsNullOrEmpty(paidiDto.SkiniName))
            return false;

        var result = await _paidiaService.AddPaidiInService(paidiDto);
        if (result)
        {
            OnPropertyChanged(nameof(Skini.Paidia));
            return true;
        }
        return false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
