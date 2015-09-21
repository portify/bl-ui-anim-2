function mLerp(%t, %a, %b)
{
    return %a + (%b - %a) * %t;
}

function mUnLerp(%v, %a, %b)
{
    return (%v - %a) / (%b - %a);
}

function SimObject::getField(%this, %name)
{
    if (%name $= "")
        return "";

    switch (stripos("_abcdefghijklmnopqrstuvwxyz", getSubStr(%name, 0, 1)))
    {
        case  0: return %this._[getSubStr(%name, 1, strlen(%name))];
        case  1: return %this.a[getSubStr(%name, 1, strlen(%name))];
        case  2: return %this.b[getSubStr(%name, 1, strlen(%name))];
        case  3: return %this.c[getSubStr(%name, 1, strlen(%name))];
        case  4: return %this.d[getSubStr(%name, 1, strlen(%name))];
        case  5: return %this.e[getSubStr(%name, 1, strlen(%name))];
        case  6: return %this.f[getSubStr(%name, 1, strlen(%name))];
        case  7: return %this.g[getSubStr(%name, 1, strlen(%name))];
        case  8: return %this.h[getSubStr(%name, 1, strlen(%name))];
        case  9: return %this.i[getSubStr(%name, 1, strlen(%name))];
        case 10: return %this.j[getSubStr(%name, 1, strlen(%name))];
        case 11: return %this.k[getSubStr(%name, 1, strlen(%name))];
        case 12: return %this.l[getSubStr(%name, 1, strlen(%name))];
        case 13: return %this.m[getSubStr(%name, 1, strlen(%name))];
        case 14: return %this.n[getSubStr(%name, 1, strlen(%name))];
        case 15: return %this.o[getSubStr(%name, 1, strlen(%name))];
        case 16: return %this.p[getSubStr(%name, 1, strlen(%name))];
        case 17: return %this.q[getSubStr(%name, 1, strlen(%name))];
        case 18: return %this.r[getSubStr(%name, 1, strlen(%name))];
        case 19: return %this.s[getSubStr(%name, 1, strlen(%name))];
        case 20: return %this.t[getSubStr(%name, 1, strlen(%name))];
        case 21: return %this.u[getSubStr(%name, 1, strlen(%name))];
        case 22: return %this.v[getSubStr(%name, 1, strlen(%name))];
        case 23: return %this.w[getSubStr(%name, 1, strlen(%name))];
        case 24: return %this.x[getSubStr(%name, 1, strlen(%name))];
        case 25: return %this.y[getSubStr(%name, 1, strlen(%name))];
        case 26: return %this.z[getSubStr(%name, 1, strlen(%name))];
    }

    return "";
}

function SimObject::setField(%this, %name, %value)
{
    switch (stripos("_abcdefghijklmnopqrstuvwxyz", getSubStr(%name, 0, 1)))
    {
        case  0: %this._[getSubStr(%name, 1, strlen(%name))] = %value;
        case  1: %this.a[getSubStr(%name, 1, strlen(%name))] = %value;
        case  2: %this.b[getSubStr(%name, 1, strlen(%name))] = %value;
        case  3: %this.c[getSubStr(%name, 1, strlen(%name))] = %value;
        case  4: %this.d[getSubStr(%name, 1, strlen(%name))] = %value;
        case  5: %this.e[getSubStr(%name, 1, strlen(%name))] = %value;
        case  6: %this.f[getSubStr(%name, 1, strlen(%name))] = %value;
        case  7: %this.g[getSubStr(%name, 1, strlen(%name))] = %value;
        case  8: %this.h[getSubStr(%name, 1, strlen(%name))] = %value;
        case  9: %this.i[getSubStr(%name, 1, strlen(%name))] = %value;
        case 10: %this.j[getSubStr(%name, 1, strlen(%name))] = %value;
        case 11: %this.k[getSubStr(%name, 1, strlen(%name))] = %value;
        case 12: %this.l[getSubStr(%name, 1, strlen(%name))] = %value;
        case 13: %this.m[getSubStr(%name, 1, strlen(%name))] = %value;
        case 14: %this.n[getSubStr(%name, 1, strlen(%name))] = %value;
        case 15: %this.o[getSubStr(%name, 1, strlen(%name))] = %value;
        case 16: %this.p[getSubStr(%name, 1, strlen(%name))] = %value;
        case 17: %this.q[getSubStr(%name, 1, strlen(%name))] = %value;
        case 18: %this.r[getSubStr(%name, 1, strlen(%name))] = %value;
        case 19: %this.s[getSubStr(%name, 1, strlen(%name))] = %value;
        case 20: %this.t[getSubStr(%name, 1, strlen(%name))] = %value;
        case 21: %this.u[getSubStr(%name, 1, strlen(%name))] = %value;
        case 22: %this.v[getSubStr(%name, 1, strlen(%name))] = %value;
        case 23: %this.w[getSubStr(%name, 1, strlen(%name))] = %value;
        case 24: %this.x[getSubStr(%name, 1, strlen(%name))] = %value;
        case 25: %this.y[getSubStr(%name, 1, strlen(%name))] = %value;
        case 26: %this.z[getSubStr(%name, 1, strlen(%name))] = %value;
    }

    return %value;
}

function interpolateGuiState(%t, %out, %a, %b)
{
    for (%i = 0; true; %i++)
    {
        %tag = %b.getTaggedField(%i);

        if (%tag $= "")
            break;

        %field = getField(%tag, 0);
        
        %vb = getFields(%tag, 1, getFieldCount(%tag));
        %va = %a.getField(%field);

        if (%va $= "")
            continue;

        %vo = "";
        %count = getWordCount(%vb);

        for (%j = 0; %j < %count; %j++)
        {
            if (%j > 0)
                %vo = %vo @ " ";

            %vo = %vo @ mLerp(%t, getWord(%va, %j), getWord(%vb, %j));
        }

        %out.setField(%field, %vo);
    }
}

function GuiControl::setGuiState(%this, %state)
{
    %shouldResize = false;
    
    %resizeX = getWord(%this.position, 0);
    %resizeY = getWord(%this.position, 1);
    %resizeW = getWord(%this.extent, 0);
    %resizeH = getWord(%this.extent, 1);

    for (%i = 0; true; %i++)
    {
        %tag = %b.getTaggedField(%i);

        if (%tag $= "")
            break;

        %field = getField(%tag, 0);
        %value = getFields(%tag, 1, getFieldCount(%tag));

        switch$ (%field)
        {
            case "position":
                %shouldResize = true;
                
                %resizeX = getWord(%value, 0);
                %resizeY = getWord(%value, 1);

            case "extent":
                %shouldResize = true;
                
                %resizeW = getWord(%value, 0);
                %resizeH = getWord(%value, 1);

            case "value":
                %this.setValue(%value);

            case "bitmap":
                %this.setBitmap(%value);

            case "text":
                %this.setText(%value);

            case "color":
                %this.setColor(%value);
        }
    }

    if (%shouldResize)
        %this.resize(%resizeX, %resizeY, %resizeW, %resizeH);
}

function GuiControl::getGuiState(%this)
{
    %state = new ScriptObject()
    {
        position = %this.getPosition();
        extent = %this.getExtent();
    };

    // this is bad
    %className = %this.getClassName();
    
    if (isFunction(%className, "getValue"))
        %state.value = %this.getValue();

    if (isFunction(%className, "getBitmap"))
        %state.bitmap = %this.getBitmap();

    if (isFunction(%className, "getText"))
        %state.text = %this.getText();

    if (isFunction(%className, "getColor"))
        %state.color = %this.getColor();

    // the proper way to do it would be something like this
    // the problem is that i'd have to do it for every control
    //
    // function GuiSwatchCtrl::getGuiState(%this)
    // {
    //     %state = Parent::getGuiState(%this);
    //     %state.color = %this.getColor();
    //     return %state;
    // }
    // 
    // function GuiBitmapCtrl::getGuiState(%this)
    // {
    //     %state = Parent::getGuiState(%this);
    //     %state.color = %this.getColor();
    //     %state.bitmap = %this.getBitmap();
    //     return %state;
    // }
}

// ta/tb = time start/end
// sa/sb = state start/end
// so = output for interpolated state
// ef/ed = easing function name/parameter
// cf/cd = callback function name/parameter

function GuiControl::animateFrom(%this, %time, %state, %ef, %ed, %cf, %cd)
{
    return %this.animateBetween(%time, %state, %this.getGuiState(), %ef, %ed, %cf, %cd);
}

function GuiControl::animateTo(%this, %time, %state, %ef, %ed, %cf, %cd)
{
    return %this.animateBetween(%time, %this.getGuiState(), %state, %ef, %ed, %cf, %cd);
}

function GuiControl::animateBetween(%this, %time, %sa, %sb, %ef, %ed, %cf, %cd)
{
    %ta = $Sim::Time;
    %tb = %ta + %time;
    %so = new ScriptObject();
    %this.updateAnimation(%ta, %tb, %sa, %sb, %so, %ef, %ed, %cf, %cd);
}

function GuiControl::updateAnimation(%this, %ta, %tb, %sa, %sb, %so, %ef, %ed, %cf, %cd)
{
    %time = mClampF(mUnLerp($Sim::Time, %ta, %tb), 0, 1);

    if (!%this.isAwake())
        %time = 1;

    if (isFunction(%ef))
        %t = call(%ef, %time, %ed);
    else
        %t = %time;

    interpolateGuiState(%t, %so, %sa, %sb);
    %this.setGuiState(%so);

    if (%time < 1)
        %this.schedule(1, "updateAnimation", %ta, %tb, %sa, %sb, %so, %ef, %ed, %cf, %cd);
    else
        call(%cf, %cd, %this);
}

// TODO:
// a p parameters for elastic

function easeInQuad(%t)
{
    return %t * %t;
}

function easeOutQuad(%t)
{
    return -1 * %t * (%t - 2);
}

function easeInOutQuad(%t)
{
    %t *= 2;
    if (%t < 1) return 0.5 * %t * %t;
    %t--;
    return -0.5 * (%t * (%t - 2) - 1);
}

function easeInCubic(%t)
{
    return %t * %t * %t;
}

function easeOutCubic(%t)
{
    %t--;
    return %t * %t * %t + 1;
}

function easeInOutCubic(%t)
{
    %t *= 2;
    if (%t < 1) return 0.5 * %t * %t * %t;
    %t -= 2;
    return 0.5 * (%t * %t * %t + 2);
}

function easeInQuart(%t)
{
    return %t * %t * %t * %t;
}

function easeOutQuart(%t)
{
    %t -= 1;
    return -1 * (%t * %t * %t * %t - 1);
}

function easeInOutQuart(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * %t * %t * %t * %t;
    }
    else
    {
        %t -= 2;
        return -0.5 * (%t * %t * %t * %t - 2);
    }
}

function easeInQuint(%t)
{
    return %t * %t * %t * %t * %t;
}

function easeOutQuint(%t)
{
    %t -= 1;
    return %t * %t * %t * %t * %t + 1;
}

function easeInOutQuint(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * %t * %t * %t * %t * %t;
    }
    else
    {
        %t -= 2;
        return 0.5 * (%t * %t * %t * %t * %t + 2);
    }
}

function easeInSine(%t)
{
    return - 1 * mCos(%t * ($pi / 2)) + 1;
}

function easeOutSine(%t)
{
    return mSin(%t * ($pi / 2));
}

function easeInOutSine(%t)
{
    return -0.5 * (mCos($pi * %t) - 1);
}

function easeInExpo(%t)
{
    if (%t == 0) return 0;
    return mPow(2, 10 * (%t - 1)) - 0.001;
}

function easeOutExpo(%t)
{
    if (%t == 1) return 1;
    return 1.001 * (-mPow(2, -10 * %t) + 1);
}

function easeInOutExpo(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * mPow(2, 10 * (%t - 1)) - 0.0005;
    }
    else
    {
        %t--;
        return 0.5 * 1.0005 * (-mPow(2, -10 * %t) + 2);
    }
}

function easeInCirc(%t)
{
    return -1 * (mSqrt(1 - mPow(%t, 2)) - 1);
}

function easeOutCirc(%t)
{
    %t -= 1;
    return mSqrt(1 - mPow(%t, 2));
}

function easeInOutCirc(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return -0.5 * (mSqrt(1 - %t * %t) - 1);
    }
    else
    {
        %t -= 2;
        return 0.5 * (mSqrt(1 - %t * %t) + 1);
    }
}

function easeInElastic(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %p = 0.3;
    %s = %p / 4;

    %t -= 1;
    return -(mPow(2, 10 * %t) * mSin((%t - %s) * (2 * $pi) / %p));
}

function easeOutElastic(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %p = 0.3;
    %s = %p / 4;

    return mPow(2, -10 * %t) * mSin((%t - %s) * (2 * $pi) / %p) + 1;
}

function easeInBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    return %t * %t * ((%s + 1) * %t - %s);
}

function easeOutBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    %t--;
    return %t * %t * ((%s + 1) * %t + %s) + 1;
}

function easeInOutBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    %s *= 1.525;
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * (%t * %t * ((%s + 1) * %t - %s));
    }
    else
    {
        %t -= 2;
        return 0.5 * (%t * %t * ((%s + 1) * %t + %s) + 2);
    }
}

function easeInBounce(%t)
{
    return 1 - easeOutBounce(1 - %t);
}

function easeOutBounce(%t)
{
    if (%t < 1 / 2.75)
    {
        return 7.5625 * %t * %t;
    }
    else if (%t < 2 / 2.75)
    {
        %t -= 1.5 / 2.75;
        return 7.5625 * %t * %t + 0.75;
    }
    else if (%t < 2.5 / 2.75)
    {
        %t -= 2.25 / 2.75;
        return 7.5625 * %t * %t + 0.9375;
    }
    else
    {
        %t -= 2.625 / 2.75;
        return 7.5625 * %t * %t + 0.984375;
    }
}

function easeInOutBounce(%t)
{
    if (%t < 0.5)
        return easeInBounce(%t * 2) * 0.5;
    else
        return easeOutBounce(%t * 2 - 1) * 0.5 + 0.5;
}
