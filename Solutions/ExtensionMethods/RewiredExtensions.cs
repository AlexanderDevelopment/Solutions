public static class RewiredExtensionMethods
	{
		public enum GameMap
		{
			Gameplay,
			Racing,
			Menu,
		}


		private static readonly Dictionary<GameMap, string> _GameModeToMapCategory = new()
		{
			{ GameMap.Gameplay, "3DMovement" },
			{ GameMap.Racing, "CarMovement" },
			{ GameMap.Menu, "UINavigation" },
		};


		private static string _previousPlayerOneGameMode;


		public static void LoadDefaultJoystickMaps(this Player input)
		{
			input.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
		}


		public static void EnableUiMap(this Player input)
		{
			if (input.id == 0)
			{
				input.controllers.maps.SetAllMapsEnabled(false);
				_previousPlayerOneGameMode = input.GetActiveMap();
				ChangeJoystickMap(input, GameMap.Menu);
			}
			else
			{
				input.controllers.maps.SetAllMapsEnabled(false);
				ChangeJoystickMap(input, GameMap.Menu);
			}
		}


		public static void DisableUiMap(this Player input)
		{
			input.ChangeJoystickMap(GameMap.Gameplay);

			if (_previousPlayerOneGameMode == _GameModeToMapCategory[GameMap.Racing] && input.id == 0)
				input.ChangeJoystickMap(GameMap.Racing);
		}


		public static void ChangeJoystickMap(this Player input, GameMap map)
		{
			input.controllers.maps.SetAllMapsEnabled(false);
			input.controllers.maps.SetMapsEnabled(true, _GameModeToMapCategory[map]);
		}


		private static string GetActiveMap(this Player input)
		{
			var maps = input.controllers.maps.GetAllMaps();

			return (from map in maps where map.enabled select map.name).FirstOrDefault();
		}
	}
