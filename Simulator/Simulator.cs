

namespace Simulator;

public static class Simulator
{
    public enum simulatorState { RUNING,STOP}
    private static Thread sim;
    private static volatile simulatorState checkStop;
    public static void  SimulatorPlay()
    {
        //sim = new Thread();
        sim.Start();
        checkStop = simulatorState.RUNING;
    }
    public static void SimulatorStop() 
    {
        checkStop = simulatorState.STOP;
    }


}