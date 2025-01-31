﻿using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Dtos.Domi;

namespace StelexarasApp.Services.Services.IServices;

public interface ITeamsService
{
    Task<bool> HasData();
    Task<bool> AddSkiniInService(SkiniDto skini);
    Task<bool> AddKoinotitaInService(KoinotitaDto koinotita);
    Task<bool> AddTomeasInService(TomeasDto tomeas);
    Task<bool> CheckStelexousXwroNameInService(IStelexosDto stelexosDto, string xwrosName);

    Task<bool> DeleteSkiniInService(int skiniId);
    Task<bool> DeleteKoinotitaInService(int koinotitaId);
    Task<bool> DeleteTomeasInService(string tomeasId);

    Task<bool> UpdateSkiniInService(SkiniDto skini);
    Task<bool> UpdateKoinotitaInService(KoinotitaDto koinotita);
    Task<bool> UpdateTomeaInService(TomeasDto tomeas);

    Task<IEnumerable<SkiniDto>> GetAllSkinesInService();
    Task<IEnumerable<KoinotitaDto>> GetAllKoinotitesInService();
    Task<IEnumerable<TomeasDto>> GetAllTomeisInService();

    Task<IEnumerable<SkiniDto>> GetSkinesAnaKoinotitaInService(string name);
    Task<IEnumerable<KoinotitaDto>> GetKoinotitesAnaTomeaInService(int name);

    Task<SkiniDto> GetSkiniByNameInService(string name);
    Task<KoinotitaDto> GetKoinotitaByNameInService(string name);
    Task<TomeasDto> GetTomeaByNameInService(string name);

    Task<IEnumerable<SkiniDto>> GetSkinesEkpaideuomenonInService();
}
