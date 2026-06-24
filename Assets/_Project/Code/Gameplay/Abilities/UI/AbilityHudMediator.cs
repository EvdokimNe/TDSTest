using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.UI.Tooltips;
using R3;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class AbilityHudMediator : ITickable, IDisposable
    {
        private readonly AbilitySystem _system;
        private readonly AbilitiesPanelView _abilitiesPanelView;
        private readonly AoeIndicatorView _aoeIndicator;
        private readonly CastHintView _castHint;
        private readonly TooltipService _tooltipService;
        private readonly List<IDisposable> _subscriptions = new();

        private List<SlotWidget> _slotViews = new List<SlotWidget>();

        public AbilityHudMediator(
            AbilitySystem system,
            AbilitiesPanelView abilitiesPanelPrefab,
            AoeIndicatorView aoeIndicator,
            CastHintView castHint,
            TooltipService tooltipService,
            UiCanvasLayersProvider canvasLayersProvider)
        {
            _system = system;
            _aoeIndicator = aoeIndicator;
            _castHint = castHint;
            _tooltipService = tooltipService;

            _abilitiesPanelView = Object.Instantiate(
                abilitiesPanelPrefab,
                canvasLayersProvider.UiLayerCanvas.transform,
                false);
        }

        public void Initialize()
        {
            var slots = _system.Slots;

            for (var i = 0; i < slots.Count; i++)
            {
                var view = _abilitiesPanelView.GetNewView();

                _slotViews.Add(view);
                var slot = slots[i];

                view.Init(slot.Config.DisplayName);
                view.SetCooldown01(0f);
                view.TooltipTrigger.Init(_tooltipService, new AbilitySlotToken { SlotIndex = i });

                _subscriptions.Add(slot.Phase.Subscribe(view.SetPhase));
            }

            _aoeIndicator.Hide();
            _castHint.Hide();
        }

        public void Tick()
        {
            for (var i = 0; i < _slotViews.Count; i++)
                _slotViews[i].SetCooldown01(_system.Slots[i].Cooldown01);

            if (_system.IsCasting)
            {
                var radius = _system.CastingConfig is AoeAbilityConfig aoe ? aoe.Radius : 1f;
                _aoeIndicator.Show(_system.CurrentAimPoint, radius);
                _castHint.Show();
            }
            else
            {
                _aoeIndicator.Hide();
                _castHint.Hide();
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _subscriptions.Count; i++)
                _subscriptions[i].Dispose();

            _subscriptions.Clear();

            if (_abilitiesPanelView != null)
                Object.Destroy(_abilitiesPanelView.gameObject);
        }
    }
}
