import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData.dark().copyWith(
        scaffoldBackgroundColor: const Color.fromARGB(255, 18, 32, 47),
      ),
      home: Scaffold(
        appBar: AppBar(
          title: Row(
            children: [
              CircleAvatar(
                backgroundColor:
                    Colors.white, // Replace with the desired background color
                radius: 40, // Adjust the radius of the circle
                child: Padding(
                  padding:
                      const EdgeInsets.all(10.0), // Adjust padding as needed
                  child: Image.asset(
                    'images/bird.jpg', // Replace with the path to your image asset
                    width: 800, // Adjust the width of the image
                    height: 1000, // Adjust the height of the image
                  ),
                ),
              ),
              SizedBox(
                  width:
                      8), // Adjust the spacing between the image and the title
              Text('Chao Mao Club'),
            ],
          ),
        ),
        drawer: Drawer(
          child: ListView(
            children: [
              UserAccountsDrawerHeader(
                accountName: Text('Duy'),
                accountEmail: Text('hotrankhanhduy16@email.com'),
                currentAccountPicture: CircleAvatar(
                  backgroundImage: AssetImage('images/avatar.jpg'),
                  // You can replace 'assets/your_picture.jpg' with the path to your image asset
                ),
              ),
              // Add your sidebar content here
              ListTile(
                title: Text('Home'),
                onTap: () {
                  // Handle home item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              ListTile(
                title: Text('Contests'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              // Add more items as needed
              ListTile(
                title: Text('Fiedltrips'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              ListTile(
                title: Text('Meetings'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              ListTile(
                title: Text('Events'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              ListTile(
                title: Text('Community'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
              ListTile(
                title: Text('Gallery'),
                onTap: () {
                  // Handle meetings item tap
                  Navigator.pop(context); // Close the sidebar
                },
              ),
            ],
          ),
        ),
        body: ListView(children: [
          MeetingOnlineQrCode(),
        ]),
      ),
    );
  }
}

class MeetingOnlineQrCode extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Container(
          width: 886,
          height: 1777,
          clipBehavior: Clip.antiAlias,
          decoration: BoxDecoration(color: Colors.white),
          child: Stack(
            children: [
              Positioned(
                left: 0,
                top: 0,
                child: Container(
                  width: 2173,
                  height: 1777,
                  child: Stack(
                    children: [
                      Positioned(
                        left: 0,
                        top: 0,
                        child: Container(
                          width: 2173,
                          height: 980.24,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image: AssetImage('images/Background.jpg'),
                              fit: BoxFit.fill,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Positioned(
                left: 135,
                top: 604,
                child: SizedBox(
                  width: 607,
                  height: 66,
                  child: Text(
                    'Meeting Online',
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      color: Colors.black,
                      fontSize: 32,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0,
                    ),
                  ),
                ),
              ),
              Positioned(
                left: 135,
                top: 844,
                child: SizedBox(
                  width: 607,
                  height: 66,
                  child: Text(
                    'Scan to check in when meeting end',
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      color: Colors.black,
                      fontSize: 32,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0,
                    ),
                  ),
                ),
              ),
              Positioned(
                left: 190,
                top: 925,
                child: Container(
                  width: 500,
                  height: 500,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                      image: AssetImage('images/QR.jpg'),
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
              ),
              Positioned(
                left: 393,
                top: 1708,
                child: Container(
                  width: 91.24,
                  height: 55,
                  decoration: ShapeDecoration(
                    color: Color(0x594AC300),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(50),
                    ),
                  ),
                ),
              ),
              Positioned(
                left: 608,
                top: 1708,
                child: Container(
                  width: 123.12,
                  height: 43,
                  child: Stack(
                    children: [
                      Positioned(
                        left: 32.66,
                        top: 0,
                        child: Container(
                          width: 25.13,
                          height: 43,
                          clipBehavior: Clip.antiAlias,
                          decoration: BoxDecoration(),
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [],
                          ),
                        ),
                      ),
                      Positioned(
                        left: 65.33,
                        top: 0,
                        child: Container(
                          width: 25.13,
                          height: 43,
                          clipBehavior: Clip.antiAlias,
                          decoration: BoxDecoration(),
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [],
                          ),
                        ),
                      ),
                      Positioned(
                        left: 97.99,
                        top: 0,
                        child: Container(
                          width: 25.13,
                          height: 43,
                          clipBehavior: Clip.antiAlias,
                          decoration: BoxDecoration(),
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [],
                          ),
                        ),
                      ),
                      Positioned(
                        left: 0,
                        top: 0,
                        child: Container(
                          width: 25.13,
                          height: 43,
                          clipBehavior: Clip.antiAlias,
                          decoration: BoxDecoration(),
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Container(
                                width: 16.75,
                                height: 35.83,
                                child: Stack(children: []),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Positioned(
                left: 0,
                top: 1511,
                child: Container(
                  width: 944.11,
                  height: 266,
                  child: Stack(
                    children: [
                      Positioned(
                        left: 0,
                        top: 0,
                        child: Container(
                          width: 886,
                          height: 266,
                          decoration: BoxDecoration(color: Color(0x591F5200)),
                        ),
                      ),
                      Positioned(
                        left: 762,
                        top: 208,
                        child: SizedBox(
                          width: 182.11,
                          height: 29,
                          child: Text(
                            '@2024 ChaoMao BirdClub',
                            style: TextStyle(
                              color: Colors.black,
                              fontSize: 24,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w400,
                              height: 0,
                              letterSpacing: 1.20,
                            ),
                          ),
                        ),
                      ),
                      Positioned(
                        left: 401,
                        top: 213,
                        child: SizedBox(
                          width: 75.48,
                          height: 35,
                          child: Text(
                            'Donate',
                            style: TextStyle(
                              color: Colors.black,
                              fontSize: 20,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Positioned(
                left: 95,
                top: 1576,
                child: SizedBox(
                  width: 696,
                  height: 68,
                  child: SingleChildScrollView(
                    scrollDirection: Axis.vertical,
                    child: Text(
                      'We welcome donations in any amount to help fund club initiatives and activities, such as the purchase of bird seed for feeding birds, necessaries for harmed bird treatments, the annual Tournaments, Field trips and other club events, the purchase of supplies and tools for our volunteer projects, etc.',
                      style: TextStyle(
                        color: Colors.black,
                        fontSize: 24,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w400,
                        height: 1.5, // Adjust line spacing as needed
                        letterSpacing: 1.20,
                      ),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
