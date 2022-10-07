using System.Diagnostics;
using System.Management;
using System.ServiceProcess;



/*
 En este apartado podemos ver los servicios que tenemos en el 
computador que con métodos y operaciones asincrónicas y con foreach paralelo y con la clase ServicesController 
y creando un objeto de este mismo podemos obtener mediante una expresión landa lo que necesitamos.
 */
static async Task Servicios()
{
    var task = new Task(() =>
    {
        Console.Clear();
        System.ServiceProcess.ServiceController[] services = System.ServiceProcess.ServiceController.GetServices();
        Parallel.ForEach(services, service =>
        {

            Console.WriteLine(@"
           Nombre del Servicio: {0}   |  Status: {1}  | El tipo del Servicio: {2}  ", 
              service.ServiceName, service.Status, service.ServiceType);
        });
        Console.WriteLine("Dele a un boton cualquiera para continuar .............");
        Console.ReadKey();
        Console.Clear();
    });
    task.Start();
    await task;

}

/*
 En este apartado podemos ver la lista de procesos de nuestra Pc que con métodos 
y operaciones asincrónicas y con foreach paralelo y con la proccess podemos obtener mediante un expresión landa lo que necesitamos.
 */

static async Task Procesos()
{
    var task2 = new Task(() => {

        Console.Clear();
        Process[] processList = Process.GetProcesses();


        Parallel.ForEach(processList, process =>
        {
            try
            {
                Console.WriteLine(@"
      Nombre del Proceso  {0} | PID: {1} | Status {2} | Uso de la memoria en Bytes  {3} | Uso de la CPU {4}",
              process.ProcessName, process.Id, process.Responding, process.PrivateMemorySize64, process.TotalProcessorTime);
            }
            catch
            {

            }

        });
        Console.WriteLine("Dele a un boton cualquiera para continuar .............");
        Console.ReadKey();
        Console.Clear();
    });
    task2.Start();
    await task2;
}

/*
 Esta parte del codigo es algo extra de la anterior tarea solo es algo que quise poner para el mennu
:):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):):)
 */

static async Task informacion()
{

    var task3 = new Task(() =>
    {
        Console.Clear();
        Console.WriteLine("El numero de nucleos que tiene el procesador de esta computadora es de " +
           " {0}.",
           Environment.ProcessorCount);

        Process[] proceso = Process.GetProcesses();

        Process ProcesoA = Process.GetCurrentProcess();

        Console.WriteLine("");
        Console.WriteLine("La cantidad de procesos que la computadora esta ejecuntadora ahora es de  " + proceso.Length);
        Console.WriteLine("");
        Console.WriteLine("El proceso actuale es : " + ProcesoA);

        Console.WriteLine("");
        ObjectQuery RAM = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(RAM);
        ManagementObjectCollection BuscarRAM = searcher.Get();

        foreach (ManagementObject Resultado in BuscarRAM)
        {
            Console.WriteLine("Total de memoria Ram : {0} KB", Resultado["TotalVisibleMemorySize"]);
            Console.WriteLine("Total de memoria RAM libre: {0} KB", Resultado["FreePhysicalMemory"]);

        }

        Console.WriteLine("");
        bool Bus;
        Bus = Environment.Is64BitProcess ? true : false;

        if (Bus == true)
        {
            Console.WriteLine("Su computador es de 64 bits");

        }
        else
        {
            Console.WriteLine("Su computador es de 32 Bits");
        }


        AutoResetEvent mainEvent = new AutoResetEvent(false);
        int workerThreads;
        int portThreads;

        ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
        Console.WriteLine("\nMaximo de hilos trabajando : \t{0}",
            workerThreads, portThreads);
        Console.WriteLine("" +
            "" +
            "" +
            "" +
            "" +
            "");
        Console.WriteLine("Dele a un boton cualquiera para continuar .............");
        Console.ReadKey();
        Console.Clear();



    });
    task3.Start();
    await task3;
}




/*
 En este apartado de servicios lo que hacemos es que buscamos todo la información de un servicio a base de su nombre como si lo seleccionáramos en específicos  
solo tenemos que poner e nombre en este utilizamos el mismo método de la vez anterior lo único que hacemos con ServiceController y lo creamos un objecto todo bien pero esta vez utilizamos un if y 
con esto prácticamente buscamos el servicos el único problema de esto es que el if va buscando en el arreglo uno por uno en arreglo hasta buscar el nombre esto consume tiempo y recursos pero es lo que me funciono.
 */

static async Task GetProccessName()
{

    var task4 = new Task(() =>
    {
        Console.Clear();
        Console.WriteLine("Escriba el proceso que desea buscar la información");
        string name = Console.ReadLine();

        Process NameP = Process.GetCurrentProcess();

        Process[] list = Process.GetProcessesByName(name);

        Parallel.ForEach(list, process =>
        {
            
                try
                {
                    Console.WriteLine(@"
      Nombre del Proceso  {0} | PID: {1} | Status {2} | Uso de la memoria en Bytes  {3} | Uso de la CPU {4}",
              process.ProcessName, process.Id, process.Responding, process.PrivateMemorySize64, process.TotalProcessorTime);
                }
                catch (Exception)
            {

                }
            
            
               
       

        });
        Console.WriteLine("Dele a un boton cualquiera para continuar .............");
        Console.ReadKey();
        Console.Clear();
    });
    task4.Start();
    await task4;
}


/*
 Este es muy sencillo solo tuve que hacer los mismo que buscar todos los servicios pero en este apartado lo que hice fue que con el mismo método de 
process este tiene un apartado de GetProcessesByName que podemos pasarle nosotros mismo a través de un string dentro pero lo que hice fue que el usuario ingresara un variable y 
que esta variable fuera a sustituir al string que debíamos poner y con esto ya tendríamos todo y algo que no recalque tengo un try catch en específicamente en estas partes de procesos porque hay procesos que Windows.
 */

static async Task GetServiceName() 
{
    var task5 = new Task(() =>
    {
        Console.Clear();
        Console.WriteLine("Escriba el Servicio que desea buscar la información");
        string name = Console.ReadLine();
        ServiceController[] services = ServiceController.GetServices();


        Parallel.ForEach(services, service =>
        {
            if (service.ServiceName == name)
            {

                Console.WriteLine(@"
                 Nombre del Servicio: {0}   |  Status: {1}  | El tipo del Servicio: {2}  ", 
                            service.ServiceName, service.Status, service.ServiceType);

            }
            else
            {

            }

        });
        
    



        Console.WriteLine("Dele a un boton cualquiera para continuar .............");
        Console.ReadKey();
        Console.Clear();
    });

    task5.Start();
    await task5;
}


int opciones = 0;
int opcion = 0;

// Aqui hice un pequeño menu para obtener cada lista o proceso para no tener un lio y hacer un menu bien chulo llamando el metodo con en su respectiva linea que le toca con un await porque son metodos asincronicos 

while (opcion == 0)
{
    Console.WriteLine("1. Ver los Servicios");
    Console.WriteLine("-------------------------");
    Console.WriteLine("2. Ver los Procesos");
    Console.WriteLine("--------------------------");
    Console.WriteLine("3. Ver la informacion de su PC");
    Console.WriteLine("--------------------------");
    Console.WriteLine("4. Obtener la informacion de un proceso en especifico por su nombre");
    Console.WriteLine("--------------------------");
    Console.WriteLine("5. Obtener la informacion de un servicio en especifico por su nombre");
    Console.WriteLine("--------------------------");
    Console.WriteLine("6. Salir del Programa");
    opciones = int.Parse(Console.ReadLine());


    
    switch (opciones)
    {

        case 0:
            Console.WriteLine("No elegio ninguna opcion vuelva intentarlo");
            break;
        case 1:

            await Servicios();

            break;

        case 2:

            await Procesos();
            break;

        case 3:
            await informacion();


            break;

        case 4:

            await GetProccessName();
            break;

        case 5:
           await GetServiceName();
            break;


        case 6:
            Console.WriteLine("Gracias por utilizar este programa ATT: Milton Garcia");
            opcion = 1;

            break;



        default:
            Console.Clear();
            Console.WriteLine("Ha ingresado un numero incorrecto vuelva a ingresar ");
            Console.Clear();
            break;
    }


    /*
     Quiero especificar en esta parte deje de decir que si utilice métodos asincrónicos o no es porque cada método es asincrónico y utilizó task para manejar las tareas y cada vez que podía utilizar 
    y veía necesario utilizaba el paralelismo en algunas sentencias, pero no lo recalque en cada parte del código solo una parte que es de la información no utilice el paralelismo lo vi innecesario 
     */


}
