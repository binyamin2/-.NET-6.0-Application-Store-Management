

 namespace Simulator;

public static class Simulator
{
    static BlApi.IBl bl = BlApi.Factory.Get();
    private static volatile bool _isRunning;

    public delegate void SimulationCompleteEventHandler();
    public static event SimulationCompleteEventHandler OnSimulationComplete;

    public delegate void UpdateEventHandler(BO.Order? order,DateTime newTime, int delay);
    public static event UpdateEventHandler OnUpdate;
    public static bool isAlreadyOpen=false;
    public static Random rnd = new Random();
    public static BO.Order? order= new BO.Order();
    public static void StartSimulation()
    {
        
        new Thread(() =>
        {
            _isRunning = true;
            while (_isRunning) 
            {
                work();
                Thread.Sleep(1000); 
            }
            OnSimulationComplete();
        }).Start();
    }
    private static void work()
    {
        
        DateTime newDate = new DateTime();
        order = bl.Order.nextOrder();
        if (order != null)
        {
            int Delay = rnd.Next(3, 11);
            newDate = DateTime.Now + new TimeSpan(0,0,Delay);
            OnUpdate(order, newDate, Delay);
            Thread.Sleep(Delay * 1000);
            if (order.Status == BO.OrderStatus.Confirmed)
            {
                bl.Order?.UpdateShip(order.ID);
            }
            else if (order.Status == BO.OrderStatus.Shiped)
            {
                bl.Order?.UpdateDelivery(order.ID);
            }
        }

    }

    public static void StopSimulation()
    {
        _isRunning = false;

    }

    public static void RegisterForSimulationCompleteEvent(SimulationCompleteEventHandler handler)
    {
        OnSimulationComplete += handler;
    }

    public static void UnregisterFromSimulationCompleteEvent(SimulationCompleteEventHandler handler)
    {
        OnSimulationComplete -= handler;
    }

    public static void RegisterForUpdateEvent(UpdateEventHandler handler)
    {
        OnUpdate += handler;
    }

    public static void UnregisterFromUpdateEvent(UpdateEventHandler handler)
    {
        OnUpdate -= handler;
    }



}