// EVM.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string.h>
#include <stdlib.h>

#define STACK_SIZE 50
#define PROGRAM_SIZE 256

#define ENGINE_STATE_FLAGS_TERMINATED   0x01
#define ENGINE_STATE_FLAGS_ERROR        0x02

typedef enum  {
    Push_1 = 0,   // Pushes a single byte onto the stack               2 bytes
    Add_U1 = 1,   // Adds two byte values on the stack                 1 bytes (pops two bytes off the stack and puts one back on - the answer)
    Write_U1 = 2, // Writes an unsigned byte to the command line      
    Call = 3,     // Call a function                                   2 bytes
    Return = 4,    // Return from a function                            
    //Terminate = 5, // Causes the engine to terminate
    //Jump,     // Should we differentiate between near and far?
} OpCode;

typedef struct {

    //The size of the program
    unsigned char programSize;

    //Here is the call stack
    unsigned char callStack[STACK_SIZE];

    //Ram for this virtual machine
    void * ram;

    //The program to execute
    unsigned char * program;

    //The top of the call stack
    unsigned int stackSize;

    //The current position in the program code
    unsigned int programCounter;

    //Flags for execution
    unsigned int flags;

} EngineState;

EngineState _engineState;

void ExecuteEngine()
{
    printf("Program  Counter: %u", _engineState.programCounter);
}

void SetError()
{
    _engineState.flags |= (ENGINE_STATE_FLAGS_ERROR | ENGINE_STATE_FLAGS_TERMINATED);
}

void Engine_Push_1(unsigned char value)
{
    if (_engineState.stackSize >= STACK_SIZE)
    {
        SetError();
    }
    else
    {
        _engineState.callStack[_engineState.stackSize] = value;

        _engineState.stackSize++;
    }
}

unsigned char Engine_Pop_1()
{
    if (_engineState.stackSize == 0)
    {
        SetError();

        return 0;
    }

    _engineState.stackSize--;

    return _engineState.callStack[_engineState.stackSize];
}

void Imp_Push_1()
{
    Engine_Push_1(_engineState.program[_engineState.programCounter + 1]);

    _engineState.programCounter += 2;
}


void Imp_Add_U1()
{
    unsigned char x = Engine_Pop_1();
    unsigned char y = Engine_Pop_1();
    
    Engine_Push_1(x + y);

    _engineState.programCounter += 1;
}

void Imp_Write_U1()
{
    unsigned char value = Engine_Pop_1();

    char buffer[10];

    sprintf(buffer, "%u", value);

    puts(buffer);

    _engineState.programCounter += 1;
}

void Imp_Call()
{
    //Get the address to jump to 
    unsigned char address = _engineState.program[_engineState.programCounter + 1];

    //Push the return address onto the stack
    Engine_Push_1(_engineState.programCounter + 2);

    //Now move execution to the function
    _engineState.programCounter = address;
}

void Imp_Return()
{
    unsigned char returnAddress;

    if (_engineState.stackSize == 0)
    {
        _engineState.flags |= ENGINE_STATE_FLAGS_TERMINATED;
    }
    else
    {
        //pop the return address off of the stack
        returnAddress = Engine_Pop_1();

        //Now move to the return address
        _engineState.programCounter = returnAddress;
    }
}

void Imp_Terminate()
{
    _engineState.flags |= ENGINE_STATE_FLAGS_TERMINATED;
}

void ExecuteProgram(unsigned char * program, unsigned char programSize, unsigned int ramSize)
{
    _engineState.programCounter = 0;
    _engineState.stackSize = 0;
    _engineState.program = program;
    _engineState.programSize = programSize;
    _engineState.flags = 0;
    _engineState.ram = malloc(ramSize);

    //Check the allocation
    if (_engineState.ram == NULL)
    {
        SetError();
    }
    
    while ((_engineState.flags & ENGINE_STATE_FLAGS_TERMINATED) == 0 && _engineState.programCounter < _engineState.programSize)
    {
        switch ((OpCode)_engineState.program[_engineState.programCounter])
        {
            case Push_1:

                Imp_Push_1();
                break;

            case Add_U1:

                Imp_Add_U1();
                break;

            case Write_U1:

                Imp_Write_U1();
                break;

            case Call:

                Imp_Call();
                break;

            case Return:

                Imp_Return();
                break;

        /*    case Terminate:

                Imp_Terminate();
                break;
*/
            default:

                puts("OpCode invalid");
                SetError();

                break;
        }
    }

    //Ditch the ram
    free(_engineState.ram);
}

unsigned long fsize(char* file)
{
    FILE * f = fopen(file, "r");

	if (f == NULL) {
		printf("No file!");
	}

    fseek(f, 0, SEEK_END);
    unsigned long len = (unsigned long)ftell(f);
    fclose(f);
    return len;
}

int _tmain(int argc, _TCHAR* argv[])
{
    char * filename = "HelloWorld.evm";
    void * buffer;

    unsigned long fileSize = fsize(filename);

    buffer = malloc(fileSize);

    if (buffer == NULL)
    {
        puts("unable to create buffer");
    }
    else
    {
        FILE * f = fopen(filename, "rb");

        if (f)
        {
            fread(buffer, 1, fileSize, f);
            
            fclose(f);

            ExecuteProgram((unsigned char *)buffer, fileSize, 2);

            if (_engineState.flags & ENGINE_STATE_FLAGS_ERROR)
            {
                puts("Completed with errors");
            }
            else
            {
                puts("Complete");
            }
        }
        else
        {
            puts("Unable to find the file");
        }

        //unsigned char program[] = {
        //    (unsigned char)Push_1, 2,   // 0
        //    (unsigned char)Push_1, 7,   // 2
        //    (unsigned char)Add_U1,      // 4
        //    (unsigned char)Write_U1,    // 5
        //    (unsigned char)Call, 12,    // 6     
        //    (unsigned char)Push_1, 99,  // 8
        //    (unsigned char)Write_U1,    // 10
        //    (unsigned char)Return,   // 11

        //    //My first function!
        //    (unsigned char)Push_1, 42,  // 12
        //    (unsigned char)Write_U1,    // 14
        //    (unsigned char)Return,      // 15
        //};

      
    }

    getchar();

	return 0;
}

