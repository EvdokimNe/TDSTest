# Project Structure

Top-down TDS (top-down shooter). DI — VContainer. Сцены и конфиги грузятся через Addressables.

---

## Дерево папок

```
_Project/
├── Code/
│   ├── Application/        # Инфраструктура запуска приложения
│   │   ├── Bootstrap/      # Точка входа, первый LifetimeScope
│   │   ├── Core/           # CoreLifetimeScope (глобальные синглтоны)
│   │   └── Loading/        # Экран загрузки (контроллер + вью)
│   ├── Gameplay/           # Игровая логика
│   │   ├── CameraFollow/   # Следование камеры за игроком
│   │   ├── Combat/         # Боевая система (см. ниже)
│   │   ├── Input/          # InputService — обёртка над Unity Input System
│   │   ├── MouseAim/       # Прицеливание мышью
│   │   ├── Movement/       # Конфиги и мотор движения
│   │   └── Player/         # PlayerProvider + системы движения/поворота персонажа
│   ├── Infrastructure/     # Платформенные сервисы
│   │   ├── CamerasProviders/   # MainCameraProvider, UiCameraProvider
│   │   ├── Configs/            # ConfigService (загрузка SO-конфигов)
│   │   └── SceneLoading/       # SceneLoadingService, SceneNames
│   ├── Shared/             # Переиспользуемые примитивы
│   │   ├── Components/     # DontDestroyOnLoad
│   │   ├── InternalIntId   # Value-type идентификатор сущности
│   │   └── InternalIntIdProvider  # Генератор уникальных Id
│   └── UI/
│       └── Infrastructure/ # Базовая UI-инфраструктура
├── Configs/                # ScriptableObject-ассеты конфигов
├── Scenes/                 # Unity-сцены
└── Settings/               # Project Settings override-ассеты
```

---

## Боевая система (`Code/Gameplay/Combat/`)

### Ключевые типы

| Тип | Роль |
|-----|------|
| `ICombatEntity` | Контракт боевой сущности (`Id`, `Team`) |
| `CombatTeam` | Enum команд (не стреляем по своим) |
| `CombatEntityRegistry` | Хранит `CombatEntityState` по `InternalIntId` |
| `CombatEntityState` | Снимок состояния: `HealthState` + `StatsContainer` + список защитных модификаторов |
| `StatsContainer` | Словарь `StatType → float`; заполняется из `StatsConfig` SO |
| `StatType` | `MaxHealth`, `Armor`, `MovementSpeed` |

### Флоу нанесения урона

```
AttackConfig (SO)
    └─▶ DamagePayloadFactory.Resolve(source, attackConfig)
            │  применяет PreResolvedModifiers (до броска — крит и т.п.)
            └─▶ DamagePayload  { Source, Damage, DamageType, DamageTags, OnHitModifiers }
                    │
                    └─▶ DamagePayload.CreateRequest(target)  →  DamageRequest
                                │
                                └─▶ DamageApplicationService.ApplyDamage(request)
                                        │  валидация (не null, не своя команда, жив)
                                        └─▶ DamagePipeline.Calculate(request, targetState)
                                                │  применяет OnHitModifiers (после броска)
                                                │  применяет DefenseModifiers цели
                                                └─▶ DamageCalculation { FinalDamage }
                                                        │
                                                        └─▶ targetState.Health.Current -= FinalDamage
                                                                └─▶ DamageResult { Applied, IsDead, FinalDamage }
```

### Модификаторы

Все модификаторы — ScriptableObject. Процессоры — чистые классы, зарегистрированные в DI.
Маппинг `ConfigType → processor` хранится в словаре внутри фабрики/пайплайна.

#### PreResolved (до вычисления урона, в `DamagePayloadFactory`)

| Config | Processor | Эффект |
|--------|-----------|--------|
| `CriticalChancePreResolvedModifierConfig` | `CriticalChancePreResolvedModifierProcessor` | Шанс крита — умножает урон на `CritMultiplier`, ставит тег `DamageTags.Critical` |

#### OnHit (во время пайплайна, после выбора цели)

| Config | Processor | Эффект |
|--------|-----------|--------|
| `PercentDamageOnHitModifierConfig` | `PercentDamageOnHitModifierProcessor` | `damage *= 1 + Percent` |

#### Defense (на стороне цели)

| Config | Processor | Эффект |
|--------|-----------|--------|
| `ArmorDamageModifierConfig` | `ArmorDamageModifierProcessor` | Снижает физический урон на основе `Armor` стата; макс. редукция — `MaxReduction` |
| `ZeroDamageModifierConfig` | `ZeroDamageModifierProcessor` | С шансом `Chance` обнуляет урон (блок/уклонение) |

### DamageContext

Мутируемый объект-аккумулятор внутри `DamagePipeline` (переиспользуется через `Reset`).
Содержит `Request` и `CurrentDamage` — именно это поле изменяют все процессоры.

### AttackConfig (SO)

```
AttackConfig
├── BaseDamage          float
├── DamageType          Physical / Fire / Poison
├── DamageTags          флаги: Critical, Projectile, Melee, Ability, Area, DamageOverTime
├── PreResolvedModifiers  List<PreResolvedModifierConfig>
└── OnHitModifiers        List<OnHitModifierConfig>
```

---

## DI / Сцены

| Scope | Что регистрирует |
|-------|-----------------|
| `BootstrapLifetimeScope` | `LoadingScreenController`, `CoreLifetimeScope` ref, `BootstrapEntryPoint` |
| `CoreLifetimeScope` | Глобальные сервисы (камеры, конфиги, загрузка сцен) |
| `GameplayLifetimeScope` | `InternalIntIdProvider`, `InputService`, `MouseAimService`, `CameraFollowSystem`, `CombatInstaller`, `PlayerMovementInstaller`, `GameplayRunner` |
| `CombatInstaller` | `CombatEntityRegistry`, `DamagePayloadFactory`, `DamagePipeline`, `DamageApplicationService`, все процессоры модификаторов |

---

## Shared-примитивы

- **`InternalIntId`** — `readonly struct` идентификатор; используется вместо `GameObject` reference в боевом реестре.
- **`InternalIntIdProvider`** — счётчик, выдаёт уникальные Id во время игровой сессии.
