import { Home } from "./components/Home";
import RideShareFrom from "./components/RideShareForm";
import RideShareView from "./components/RideShareView";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/form',
    element: <RideShareFrom />
  },
  {
    path: '/view',
    element: <RideShareView />
  }
];

export default AppRoutes;
