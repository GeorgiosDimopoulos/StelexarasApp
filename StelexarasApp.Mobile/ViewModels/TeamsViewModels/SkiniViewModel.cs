using AutoMapper;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StelexarasApp.Mobile.ViewModels.TeamsViewModels;

public class SkiniViewModel : INotifyPropertyChanged
{
    private readonly IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> _paidiaService;
    private readonly IMapper mapper;

    public Skini Skini { get; set; }

    public SkiniViewModel(SkiniResponse skini, IPaidiService<CreatePaidiRequest, UpdatePaidiRequest, DeletePaidiRequest, PaidiResponse> paidiaService)
    {
        mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SkiniResponse, Skini>();
            cfg.CreateMap<PaidiResponse, Paidi>();
        }).CreateMapper();

        this.Skini = mapper.Map<Skini>(skini);
        _paidiaService = paidiaService;
    }

    public async Task<bool> AddPaidiAsync(CreatePaidiRequest paidiDto)
    {
        if (string.IsNullOrEmpty(paidiDto.FullName) || string.IsNullOrEmpty(paidiDto.SkiniName))
            return false;

        var result = await _paidiaService.CreatePaidiInService(paidiDto);
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
