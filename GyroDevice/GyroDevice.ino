boolean connectState = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  while(!Serial){
    ;
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  
  if(Serial.available() > 0){
    //
    byte data = Serial.read();
    if(data == 0b00100000){
      
      connectState = true;
      byte recevie = 0b00100001;
      
      Serial.write(recevie);
      Serial.write('\n');
      //Serial.println("OK");
    }else{
      Serial.print("Fail : ");
      Serial.println(data);
    }
  }
  if(connectState){
    //
  }
}
