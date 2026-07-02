# Copilot Instructions

## Project Guidelines
- In AutoMapper 16.x, AddAutoMapper(Type) and AddAutoMapper(Assembly) overloads are removed. The correct way is: services.AddAutoMapper(cfg => cfg.AddMaps(typeof(SomeProfile).Assembly))