namespace Tests.Stubs;

public static class HostSpecificationsFactory
{
    public static HostSpecifications Create(List<int> virtualisationFactors, int memory, int storage)
    {
        List<VirtualisationFactor> processors = new();
        virtualisationFactors.ForEach(vf =>
        {
            processors.Add(new VirtualisationFactor(CreateProcessor(1, 1), vf));
        });

        return new HostSpecifications(processors, memory, storage);
    }


    public static Processor CreateProcessor(int cores, int threads)
    {
        return new Processor("processor", cores, threads);
    }
}
