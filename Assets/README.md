# TDS — тестовое задание

Top-down shooter на Unity. Весь код проекта — в `_Project/Code`.

## Стек

- **VContainer** — DI
- **UniTask** — асинхронность
- **R3** — реактивность
- **uPools** — пулинг

## Архитектура

Выбран data-oriented подход/псевдо ecs - так как часть механик хорошо ложится на такой подход.

Инициализация разделена на этапы, для маштабирования
Bootstrap → Core → GameStateMachine → GameplayState
                     ├─ загрузка конфигов (Addressables)
                     ├─ загрузка сцены
                     └─ GameplayLifetimeScope → GameplayRunner

GameplayRunner - отвечает за порядок выполнения "систем".

Некоторые модули, например ScreenManager или GameStateMachine реализованы в их минималистичном виде.

Конфигурируемые системы как способности или атаки, постороены на SO для примера.

Есть сгенерированное описание Assets\PROJECT_STRUCTURE_GENERATED.md которое более подробно расскрывает устройство боевой системы.

