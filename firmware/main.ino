#include <Arduino.h>

#define TRIGGER_DROP_1 0
#define TRIGGER_DROP_2 4
#define TRIGGER_SHUTTER 1
#define TRIGGER_FLASH1 2
#define TRIGGER_FLASH2 3
#define TRIGGER_MAX 5

#define TRIGGER_PIN_DROP_1 2
#define TRIGGER_PIN_DROP_2 TRIGGER_PIN_DROP_1
#define TRIGGER_PIN_SHUTTER 3
#define TRIGGER_PIN_FLASH1 4
#define TRIGGER_PIN_FLASH2 5

#define DEBUG 1

struct Trigger {
    int pin;
    int triggerDelay;
    int triggerDuration;

    long startedAt;

    bool isTriggered;
    bool isDone;

    Trigger()
    : pin(0)
    , isTriggered(false)
    , isDone(false)
    , startedAt(0)
    {
    }

    void SetPin(int p) 
    {
        pin = p;
        pinMode(pin, OUTPUT);
    }

    void Reset(int tDelay, int tDuration) 
    {
        isTriggered = false;
        isDone = false;
        startedAt = 0;
        triggerDelay = tDelay;
        triggerDuration = tDuration;
    }

    void Update(long elapsed) 
    {
        if (!isTriggered && elapsed > triggerDelay) {
            digitalWrite(pin, HIGH);
#ifdef DEBUG
            Serial.println("Pin " + String(pin) + " HIGH (" + String(elapsed) + ")");
#endif
            startedAt = elapsed;
            isTriggered = true;
            return;
        }

        if (isTriggered && !isDone && elapsed > (startedAt + triggerDuration)) {
            digitalWrite(pin, LOW);
#ifdef DEBUG
            Serial.println("Pin " + String(pin) + " LOW (" + String(elapsed) + ")");
#endif
            isDone = true;
            return;
        }
    }
};

struct Plan {
    int32_t drop1Duration;       //du0
    int32_t drop1Delay;          //dd0
    int32_t drop2Duration;       //du1
    int32_t drop2Delay;          //dd1
    int32_t shutterDelay;       //sd0
    int32_t shutterDuration;    //su0
    int32_t flashDelay;         //fd0
    int32_t flashDuration;      //fu0

    bool isExecuting;
    long startedAt;

    Trigger triggers[TRIGGER_MAX];

    Plan() 
    : isExecuting(false)
    , startedAt(0)
    {
        triggers[TRIGGER_DROP_1].SetPin(TRIGGER_PIN_DROP_1);
        triggers[TRIGGER_DROP_2].SetPin(TRIGGER_PIN_DROP_2);
        triggers[TRIGGER_SHUTTER].SetPin(TRIGGER_PIN_SHUTTER);
        triggers[TRIGGER_FLASH1].SetPin(TRIGGER_PIN_FLASH1);
        triggers[TRIGGER_FLASH2].SetPin(TRIGGER_PIN_FLASH2);
    }

    void exec() {
        isExecuting = true;
        triggers[TRIGGER_DROP_1].Reset(drop1Delay, drop1Duration);
        triggers[TRIGGER_DROP_2].Reset(drop2Delay, drop2Duration);
        triggers[TRIGGER_SHUTTER].Reset(shutterDelay, shutterDuration);
        triggers[TRIGGER_FLASH1].Reset(flashDelay, flashDuration);
        triggers[TRIGGER_FLASH2].Reset(flashDelay, flashDuration);

        startedAt = millis();
    }

    void update() {
        if(!isExecuting) {
            return;
        }

        long now = millis();
        long elapsed = now - startedAt;

        bool allDone = true;
        for(int i = 0; i < TRIGGER_MAX; i++) {
            triggers[i].Update(elapsed);
            allDone = allDone && triggers[i].isDone;
        }

        if(allDone) {
            isExecuting = false;
            Serial.println("Done executing plan after " + String(millis() - startedAt) + "ms");
        }
    }
};

Plan currentPlan;

void setup() {
    Serial.begin(9600);
}

void loop() {
    if(currentPlan.isExecuting) {
        currentPlan.update();
    } else {
        delay(100);
        readCommands();
    }
}

void readCommands() {
    String serialInput = "";
    String cmd = "";
    int param = 0;
    while (Serial.available() > 0) {
        serialInput = Serial.readStringUntil('&');
        
        if(serialInput.length() < 3) {
            Serial.println("Invalid input " + serialInput);
            return;
        }

        cmd = serialInput.substring(0, 3);
        if(serialInput.length() > 3) {
            param = serialInput.substring(3).toInt();
        } else {
            param = 0;
        }

        execCommand(cmd, param);
    }
}

void execCommand(String cmd, int param) 
{
    if(cmd.equals("dd0")) {
        currentPlan.drop1Delay = param;
    } else if(cmd.equals("du0")) {
        currentPlan.drop1Duration = param;
    } else if(cmd.equals("dd1")) {
        currentPlan.drop2Delay = param;
    } else if(cmd.equals("du1")) {
        currentPlan.drop2Duration = param;
    } else if(cmd.equals("sd0")) {
        currentPlan.shutterDelay = param;
    } else if(cmd.equals("su0")) {
        currentPlan.shutterDuration = param;
    } else if(cmd.equals("fd0")) {
        currentPlan.flashDelay = param;
    } else if(cmd.equals("fu0")) {
        currentPlan.flashDuration = param;
    } else if(cmd.equals("exe")) {
        execPlan();
    } else {
        Serial.println("Unknown command " + cmd);
    }
}

void execPlan() {
    Serial.println("Executing plan:");
    Serial.println("Drop1Delay: " + String(currentPlan.drop1Delay));
    Serial.println("Drop1Duration: " + String(currentPlan.drop1Duration));
    Serial.println("Drop2Delay: " + String(currentPlan.drop2Delay));
    Serial.println("Drop2Duration: " + String(currentPlan.drop2Duration));
    Serial.println("ShutterDelay: " + String(currentPlan.shutterDelay));
    Serial.println("ShutterDuration: " + String(currentPlan.shutterDuration));
    Serial.println("FlashDelay: " + String(currentPlan.flashDelay));
    Serial.println("FlashDuration: " + String(currentPlan.flashDuration));

    currentPlan.exec();
}